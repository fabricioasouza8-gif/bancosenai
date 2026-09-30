using BancoSENAIAPI.Data;
using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarteiraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarteiraController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/Carteira
        [HttpGet]
        public async Task<IActionResult> ListarTodas()
        {
            var carteiras = await _context.Carteira.ToListAsync();

            return Ok(carteiras);
        }

        // GET: api/v1/Carteira/1
        [HttpGet("{numero}")]
        public async Task<IActionResult> ConsultarPorId(int numero)
        {
            var carteira = await _context.Carteira
                .FirstOrDefaultAsync(c => c.NumeroCarteira == numero);

            if (carteira == null)
            {
                return NotFound(new
                {
                    message = "Carteira não encontrada."
                });
            }

            return Ok(carteira);
        }

        // POST: api/v1/Carteira
        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] Carteira novaCarteira)
        {
            if (novaCarteira == null)
            {
                return BadRequest(new
                {
                    message = "Dados da carteira são obrigatórios."
                });
            }

            if (novaCarteira.ApetiteCarteira < 0)
            {
                return BadRequest(new
                {
                    message = "O apetite da carteira não pode ser negativo."
                });
            }

            var carteiraExistente = await _context.Carteira
                .FirstOrDefaultAsync(c =>
                    c.NumeroCarteira == novaCarteira.NumeroCarteira);

            if (carteiraExistente != null)
            {
                return BadRequest(new
                {
                    message = "Este número de carteira já existe."
                });
            }

            await _context.Carteira.AddAsync(novaCarteira);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(ConsultarPorId),
                new { numero = novaCarteira.NumeroCarteira },
                novaCarteira
            );
        }

        // PUT: api/v1/Carteira/1
        [HttpPut("{numero}")]
        public async Task<IActionResult> Atualizar(
            int numero,
            [FromBody] Carteira carteiraAtualizada)
        {
            var carteira = await _context.Carteira
                .FirstOrDefaultAsync(c => c.NumeroCarteira == numero);

            if (carteira == null)
            {
                return NotFound(new
                {
                    message = "Carteira não encontrada."
                });
            }

            if (carteiraAtualizada.ApetiteCarteira < 0)
            {
                return BadRequest(new
                {
                    message = "O apetite da carteira não pode ser negativo."
                });
            }

            carteira.NomeCarteira = carteiraAtualizada.NomeCarteira;
            carteira.ApetiteCarteira = carteiraAtualizada.ApetiteCarteira;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/v1/Carteira/1
        [HttpDelete("{numero}")]
        public async Task<IActionResult> Apagar(int numero)
        {
            var carteira = await _context.Carteira
                .FirstOrDefaultAsync(c => c.NumeroCarteira == numero);

            if (carteira == null)
            {
                return NotFound(new
                {
                    message = "Carteira não encontrada."
                });
            }

            _context.Carteira.Remove(carteira);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Carteira apagada com sucesso."
            });
        }
    }
}