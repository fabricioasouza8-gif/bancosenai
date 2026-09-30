using BancoSENAIAPI.Data;
using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly string _caminhoRaiz =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "ClienteArquivos"
            );

        public DocumentoController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/v1/Documento/upload/1
        [HttpPost("upload/{codigoCliente}")]
        public async Task<IActionResult> AnexarArquivo(
            int codigoCliente,
            IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                return BadRequest(new
                {
                    message = "Nenhum arquivo foi enviado."
                });
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.Codigo == codigoCliente);

            if (cliente == null)
            {
                return NotFound(new
                {
                    message = "Cliente não encontrado."
                });
            }

            string pastaCliente = Path.Combine(
                _caminhoRaiz,
                codigoCliente.ToString()
            );

            if (!Directory.Exists(pastaCliente))
            {
                Directory.CreateDirectory(pastaCliente);
            }

            string extensao = Path.GetExtension(arquivo.FileName);

            string nomeOriginal =
                Path.GetFileNameWithoutExtension(arquivo.FileName);

            string novoNome =
                $"{codigoCliente}_{nomeOriginal}_{Guid.NewGuid()}{extensao}";

            string caminhoFinal =
                Path.Combine(pastaCliente, novoNome);

            using (var stream = new FileStream(
                caminhoFinal,
                FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            var documento = new DocumentoMetadados
            {
                Nome = nomeOriginal,
                Extensao = extensao,
                Caminho = caminhoFinal,
                CodigoCliente = codigoCliente
            };

            await _context.Documento.AddAsync(documento);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(DownloadDocumento),
                new { id = documento.Id },
                new
                {
                    mensagem = "Documento anexado com sucesso!",
                    id = documento.Id,
                    arquivoSalvo = novoNome
                }
            );
        }

        // GET: api/v1/Documento/listar/1
        [HttpGet("listar/{codigoCliente}")]
        public async Task<IActionResult> ListarDocumentos(
            int codigoCliente)
        {
            var documentos = await _context.Documento
                .Where(d => d.CodigoCliente == codigoCliente)
                .ToListAsync();

            if (!documentos.Any())
            {
                return NotFound(new
                {
                    message = "Nenhum documento encontrado para este cliente."
                });
            }

            return Ok(documentos);
        }

        // GET: api/v1/Documento/download/1
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadDocumento(int id)
        {
            var documento = await _context.Documento
                .FirstOrDefaultAsync(d => d.Id == id);

            if (documento == null)
            {
                return NotFound(new
                {
                    message = "Documento não encontrado."
                });
            }

            if (!System.IO.File.Exists(documento.Caminho))
            {
                return NotFound(new
                {
                    message = "Arquivo físico não encontrado."
                });
            }

            var fileBytes =
                await System.IO.File.ReadAllBytesAsync(
                    documento.Caminho
                );

            var nomeArquivo =
                documento.Nome + documento.Extensao;

            return File(
                fileBytes,
                "application/octet-stream",
                nomeArquivo
            );
        }

        // DELETE: api/v1/Documento/excluir/1
        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> ExcluirDocumento(int id)
        {
            var documento = await _context.Documento
                .FirstOrDefaultAsync(d => d.Id == id);

            if (documento == null)
            {
                return NotFound(new
                {
                    message = "Documento não encontrado."
                });
            }

            if (System.IO.File.Exists(documento.Caminho))
            {
                System.IO.File.Delete(documento.Caminho);
            }

            _context.Documento.Remove(documento);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Documento excluído com sucesso."
            });
        }
    }
}