using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Models
{
    public class Cliente
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cpf { get; set; }

        public int NumeroAgencia { get; set; }

        public decimal Saldo { get; set; } = 0.0m;
    }
}