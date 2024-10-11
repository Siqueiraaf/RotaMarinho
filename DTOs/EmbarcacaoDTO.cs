namespace RotaMarinho.DTOs
{
    public class EmbarcacaoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Capacidade { get; set; }
        public string TipoAlocacao { get; set; }
        public decimal PrecoPorHora { get; set; }
        public decimal PrecoPorTrabalho { get; set; }
        public string Status { get; set; }
    }
}