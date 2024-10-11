using RotaMarinho.DTOs;

namespace RotaMarinho.Application.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetAllClientesAsync();
        Task<ClienteDTO> GetClienteByIdAsync(int id);
        Task<ClienteDTO> GetClienteByNomeAsync(string nome);
        Task<ClienteDTO> GetClienteByCPFAsync(string CPF);
        Task AddClienteAsync(ClienteDTO clienteDTO);
        Task UpdateClienteAsync(int id, ClienteDTO clienteDTO);
        Task DeleteClienteAsync(int id);
    }
}