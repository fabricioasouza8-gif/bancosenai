using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public required string NomeUsuario { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
