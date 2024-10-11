using RotaMarinho.Models;

namespace RotaMarinho.Domain.Repositories
{
    public interface IEmbarcacaoRepository
    {
        Task<IEnumerable<Embarcacao>> GetAllEmbarcacaoAsync();
        Task<Embarcacao> GetEmbarcacaoByIdAsync(int id);
        Task AddEmbarcacaoAsync(Embarcacao embarcacao);
        Task UpdateEmbarcacaoAsync(Embarcacao Embarcacao);
        Task DeleteEmbarcacaoAsync(int id);
        Task<IEnumerable<Embarcacao>> GetEmbarcacoesDisponiveisAsync(DateTime dataInicio, DateTime dataFim);
    }
}