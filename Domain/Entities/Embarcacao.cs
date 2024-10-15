namespace RotaMarinho.Entities
{
    public class Embarcacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public int Capacidade { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public decimal PrecoPorHora { get; set; }
        public decimal PrecoPorTrabalho { get; set; }
        public bool EstaDisponivel()
    {
        return Status == "Disponível";
    }
    }
}