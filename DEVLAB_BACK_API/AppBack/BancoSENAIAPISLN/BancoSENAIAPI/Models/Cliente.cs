namespace BancoSENAIAPI.Models
{
    public class Cliente
    {
        public int CodigoCliente { get; set; }

        public string NomeCliente { get; set; }

        public string CPF { get; set; }

        public decimal Saldo { get; set; } = 0.0m;

        public int NumeroAgencia { get; set; }

<<<<<<< Updated upstream
        public decimal SaldoTotal { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }

        public string Endereco { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }
=======

>>>>>>> Stashed changes
    }
}