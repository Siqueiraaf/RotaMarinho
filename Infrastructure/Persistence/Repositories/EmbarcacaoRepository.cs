using Microsoft.EntityFrameworkCore;
using RotaMarinho.Models; // Certifique-se que este namespace é o correto para as entidades

namespace RotaMarinho.Infrastructure.Persistence.Repositories
{
    public class EmbarcacaoRepository : IEmbarcacaoRepository
    {
        private readonly AppDbContext _context;

        public EmbarcacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Embarcacao>> GetAllEmbarcacaoAsync()
        {
            return await _context.Embarcacoes.ToListAsync();
        }

        public async Task<Embarcacao> GetEmbarcacaoByIdAsync(int id)
        {
            return await _context.Embarcacoes.FindAsync(id);
        }

        public async Task AddEmbarcacaoAsync(Embarcacao embarcacao) // Renomeado para seguir a convenção
        {
            await _context.Embarcacoes.AddAsync(embarcacao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmbarcacaoAsync(Embarcacao embarcacao)
        {
            _context.Embarcacoes.Update(embarcacao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmbarcacaoAsync(int id)
        {
            var embarcacao = await _context.Embarcacoes.FindAsync(id);
            if (embarcacao != null)
            {
                _context.Embarcacoes.Remove(embarcacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Embarcacao>> GetEmbarcacoesDisponiveisAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Embarcacoes
                                 .Where(e => e.Status == "Disponível")
                                 .ToListAsync();
        }

    }
}
