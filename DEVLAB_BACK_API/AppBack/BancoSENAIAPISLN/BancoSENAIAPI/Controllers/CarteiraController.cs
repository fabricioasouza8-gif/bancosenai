using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarteiraController : ControllerBase
    {
        private static List<Carteira> _carteira = new List<Carteira>
        {
            new Carteira
            {
                NumeroCarteira = 1,
                NomeCarteira = "Agro",
                ApetiteCarteira = 100000
            },
            new Carteira
            {
                NumeroCarteira = 2,
                NomeCarteira = "Varejo",
                ApetiteCarteira = 15000000
            },
            new Carteira
            {
                NumeroCarteira = 3,
                NomeCarteira = "Atacado",
                ApetiteCarteira = 20000000
            }


        };
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_carteira);
        }


        // GET: api/Carteira/1
        [HttpGet("{numeroCarteira}")]
        public IActionResult Get(int numeroCarteira)
        {
            var carteira = _carteira.FirstOrDefault(
                c => c.NumeroCarteira == numeroCarteira
            );

            if (carteira == null)
            {
                return NotFound("Carteira não encontrada.");
            }

            return Ok(carteira);
        }
    }
        
    }
