using BancoSENAIAPI.Models;
<<<<<<< Updated upstream
using Microsoft.AspNetCore.Http;
=======
>>>>>>> Stashed changes
using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
<<<<<<< Updated upstream
    [Route("api/[controller]")]
    [ApiController]
    public class CarteiraController : ControllerBase
    {
        private static List<Carteira> _carteira = new List<Carteira>
=======
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarteiraController : ControllerBase
    {
        private static List<Carteira> _carteiras = new List<Carteira>
>>>>>>> Stashed changes
        {
            new Carteira
            {
                NumeroCarteira = 1,
                NomeCarteira = "Agro",
<<<<<<< Updated upstream
                ApetiteCarteira = 100000
            },
=======
                ApetiteCarteira = 1000000
            },

>>>>>>> Stashed changes
            new Carteira
            {
                NumeroCarteira = 2,
                NomeCarteira = "Varejo",
<<<<<<< Updated upstream
                ApetiteCarteira = 15000000
            },
=======
                ApetiteCarteira = 1500000
            },

>>>>>>> Stashed changes
            new Carteira
            {
                NumeroCarteira = 3,
                NomeCarteira = "Atacado",
<<<<<<< Updated upstream
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
=======
                ApetiteCarteira = 2000000
            }
        };

        [HttpGet]
        public IActionResult ListarTodas()
        {
            return Ok(_carteiras);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Carteira novaCarteira)
        {
            if (_carteiras.Any(c => c.NumeroCarteira == novaCarteira.NumeroCarteira))
            {
                return BadRequest(new { message = "Este número de carteira já existe." });
            }

            if (novaCarteira.ApetiteCarteira < 0)
            {
                return BadRequest(new { message = "O apetite da carteira não pode ser negativo." });
            }

            _carteiras.Add(novaCarteira);

            return Created("", novaCarteira);
        }
        [HttpPut("{numero}")]
        public IActionResult Atualizar(int numero, [FromBody] Carteira carteiraAtualizada)
        {
            var carteira = _carteiras.FirstOrDefault(c => c.NumeroCarteira == numero);

            if (carteira == null)
            {
                return NotFound(new { message = "Carteira não encontrada." });
            }

            if (carteiraAtualizada.ApetiteCarteira < 0)
            {
                return BadRequest(new { message = "O apetite da carteira não pode ser negativo." });
            }

            carteira.NomeCarteira = carteiraAtualizada.NomeCarteira;
            carteira.ApetiteCarteira = carteiraAtualizada.ApetiteCarteira;

            return Ok(carteira);
        }
        [HttpDelete("{numero}")]
        public IActionResult Apagar(int numero)
        {
            var carteira = _carteiras.FirstOrDefault(c => c.NumeroCarteira == numero);

            if (carteira == null)
            {
                return NotFound(new { message = "Carteira não encontrada." });
            }

            _carteiras.Remove(carteira);

            return Ok(new { message = "Carteira apagada com sucesso." });
        }








    }

}
>>>>>>> Stashed changes
