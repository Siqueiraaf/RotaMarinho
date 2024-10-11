using RotaMarinho.Models;

namespace RotaMarinho.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public Embarcacao Embarcacao{ get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Status { get; set; }
        public decimal Valor { get; set; }
        public decimal CalcularCustoTotal()
        {   
            var duracao = (DataFim - DataInicio).TotalHours;
            return (decimal)duracao * Embarcacao.PrecoPorHora;
        }
    }
}