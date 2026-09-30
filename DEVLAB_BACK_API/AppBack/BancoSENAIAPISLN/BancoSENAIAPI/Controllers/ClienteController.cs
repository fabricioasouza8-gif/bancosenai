using BancoSENAIAPI.Data;
using BancoSENAIAPI.Models;
using BancoSENAIAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/Cliente
        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var clientes = await _context.Cliente.ToListAsync();

            return Ok(clientes);
        }

        // GET: api/v1/Cliente/1
        [HttpGet("{codigo}")]
        public async Task<IActionResult> ConsultarPorCodigo(int codigo)
        {
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.Codigo == codigo);

            if (cliente == null)
            {
                return NotFound(new
                {
                    message = "Cliente não encontrado."
                });
            }

            return Ok(cliente);
        }

        // POST: api/v1/Cliente
        [HttpPost]
        public async Task<IActionResult> Cadastrar(
            [FromBody] Cliente novoCliente)
        {
            if (novoCliente == null)
            {
                return BadRequest(new
                {
                    message = "Dados do cliente são obrigatórios."
                });
            }

            if (string.IsNullOrWhiteSpace(novoCliente.Nome))
            {
                return BadRequest(new
                {
                    message = "O nome do cliente é obrigatório."
                });
            }

            if (string.IsNullOrWhiteSpace(novoCliente.Cpf))
            {
                return BadRequest(new
                {
                    message = "O CPF do cliente é obrigatório."
                });
            }

            string cpf = new string(
                novoCliente.Cpf
                    .Where(char.IsDigit)
                    .ToArray()
            );

            if (!FormService.ValidarCPF(cpf))
            {
                return BadRequest(new
                {
                    message = "O CPF do cliente é inválido."
                });
            }

            var clienteExistente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.Cpf == cpf);

            if (clienteExistente != null)
            {
                return BadRequest(new
                {
                    message = "Este CPF já está cadastrado."
                });
            }

            novoCliente.Cpf = cpf;

            await _context.Cliente.AddAsync(novoCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(ConsultarPorCodigo),
                new { codigo = novoCliente.Codigo },
                novoCliente
            );
        }

        // PUT: api/v1/Cliente/1
        [HttpPut("{codigo}")]
        public async Task<IActionResult> Alterar(
            int codigo,
            [FromBody] Cliente clienteAtualizado)
        {
            var clienteExistente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.Codigo == codigo);

            if (clienteExistente == null)
            {
                return NotFound(new
                {
                    message = "Cliente não encontrado."
                });
            }

            if (string.IsNullOrWhiteSpace(clienteAtualizado.Nome))
            {
                return BadRequest(new
                {
                    message = "O nome do cliente é obrigatório."
                });
            }

            if (string.IsNullOrWhiteSpace(clienteAtualizado.Cpf))
            {
                return BadRequest(new
                {
                    message = "O CPF do cliente é obrigatório."
                });
            }

            string cpf = new string(
                clienteAtualizado.Cpf
                    .Where(char.IsDigit)
                    .ToArray()
            );

            if (!FormService.ValidarCPF(cpf))
            {
                return BadRequest(new
                {
                    message = "O CPF do cliente é inválido."
                });
            }

            clienteExistente.Nome = clienteAtualizado.Nome;
            clienteExistente.Cpf = cpf;
            clienteExistente.NumeroAgencia = clienteAtualizado.NumeroAgencia;
            clienteExistente.Saldo = clienteAtualizado.Saldo;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/v1/Cliente/1
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Excluir(int codigo)
        {
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.Codigo == codigo);

            if (cliente == null)
            {
                return NotFound(new
                {
                    message = "Cliente não encontrado."
                });
            }

            _context.Cliente.Remove(cliente);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Cliente excluído com sucesso."
            });
        }
    }
}