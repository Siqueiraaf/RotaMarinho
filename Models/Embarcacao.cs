namespace RotaMarinho.Models
{
    public class Embarcacao
    {
        // Tabelas do banco de dados
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public int Capacidade { get; set; }
        public string TipoAlocacao { get; set; } // Passeio ou Serviço
        public decimal? PrecoPorHora { get; set; }
        public decimal? PrecoPorTrabalho { get; set; }
        public string Status { get; set; } // Disponível, Reservado, Manutenção
    }
}