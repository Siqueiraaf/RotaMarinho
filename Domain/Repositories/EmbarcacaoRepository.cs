using RotaMarinho.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using RotaMarinho.Models;

namespace RotaMarinho.Domain.Repositories
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
            return await _context.Embarcacoes.ToListAsync(); // Verifique se isso está retornando o tipo correto
        }

        public async Task<Embarcacao> GetEmbarcacaoByIdAsync(int id)
        {
            var embarcacao = await _context.Embarcacoes.FindAsync(id);
            if (embarcacao == null)
            {
                throw new KeyNotFoundException($"Embarcação com ID {id} não encontrada.");
            }
            return embarcacao;
        }

        public async Task AddEmbarcacaoAsync(Embarcacao embarcacao)
        {
            try
            {
                await _context.Embarcacoes.AddAsync(embarcacao); // Corrigido para AddAsync
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Trate a exceção conforme necessário (ex: log)
                throw new Exception("Erro ao adicionar embarcação.", ex);
            }
        }

        public async Task UpdateEmbarcacaoAsync(Embarcacao embarcacao)
        {
            try
            {
                _context.Embarcacoes.Update(embarcacao);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Trate a exceção conforme necessário (ex: log)
                throw new Exception("Erro ao atualizar embarcação.", ex);
            }
        }

        public async Task DeleteEmbarcacaoAsync(int id)
        {
            var embarcacao = await _context.Embarcacoes.FindAsync(id);
            if (embarcacao != null)
            {
                _context.Embarcacoes.Remove(embarcacao);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Embarcação com ID {id} não encontrada.");
            }
        }

        public async Task<IEnumerable<Embarcacao>> GetEmbarcacoesDisponiveisAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _context.Embarcacoes
                .Where(b => b.Status == "Disponível" &&
                            !_context.Reservas.Any(r => r.EmbarcacaoId == b.Id &&
                            (r.DataInicio <= dataFim && r.DataFim >= dataInicio)))
                .ToListAsync(); // Verifique se isso está retornando o tipo correto
        }
    }
}
