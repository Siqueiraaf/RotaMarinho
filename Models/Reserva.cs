namespace RotaMarinho.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int EmbarcacaoId { get; set; }
        public Embarcacao Embarcacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Tipo { get; set; } // Passeio ou Serviço
        public decimal Valor { get; set; }
        public string Status { get; set; } // Confirmada, Cancelada
    }
}