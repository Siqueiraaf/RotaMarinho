using RotaMarinho.Entities;


namespace RotaMarinho.Repositories
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> GetAllResevaAsync();
        Task<Reserva> GetEmbarcacaoByIdAsync(int id);
        Task AddEmbarcacaoAsync(Reserva reserva);
        Task UpdateEmbarcacaoAsync(Reserva reserva);
        Task DeleteEmbarcacaoAsync(int id);
    }
}