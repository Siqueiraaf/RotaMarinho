using RotaMarinho.Models;

namespace RotaMarinho.Infrastructure.Persistence.Repositories
{
    public interface IEmbarcacaoRepository
    {
        Task<IEnumerable<Embarcacao>> GetAllEmbarcacaoAsync();
        Task<Embarcacao> GetEmbarcacaoByIdAsync(int id);
        Task AddEmbarcacaoAsync(Embarcacao embarcacao);
        Task UpdateEmbarcacaoAsync(Embarcacao embarcacao);
        Task DeleteEmbarcacaoAsync(int id);
        Task<IEnumerable<Embarcacao>> GetEmbarcacoesDisponiveisAsync(DateTime startDate, DateTime endDate);
    }
}