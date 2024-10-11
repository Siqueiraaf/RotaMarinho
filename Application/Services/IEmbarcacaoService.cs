using RotaMarinho.DTOs;

namespace RotaMarinho.Application.Services
{
    public interface IEmbarcacaoService
    {
        Task<IEnumerable<EmbarcacaoDTO>> GetAllEmbarcacoesAsync();
        Task<EmbarcacaoDTO> GetEmbarcacaoByIdAsync(int id);
        Task AddEmbarcacaoAsync(EmbarcacaoDTO embarcacaoDTO);
        Task UpdateEmbarcacaoAsync(int id, EmbarcacaoDTO embarcacaoDTO);
        Task DeleteEmbarcacaoAsync(int id);
        Task<IEnumerable<EmbarcacaoDTO>> GetEmbarcacoesDisponiveisAsync(DateTime dataInicio, DateTime dataFim);
    }
}

