using System.Data;

namespace BancoSENAIAPI.Dtos
{
    public class LoginResponseDto
    {
        public required string Token { get; set; }
        public DateTime ExpiraEm {  get; set; }
    }
}
