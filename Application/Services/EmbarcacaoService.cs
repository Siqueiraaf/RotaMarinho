using RotaMarinho.Infrastructure.Persistence;
using RotaMarinho.DTOs;
using RotaMarinho.Models;
using Microsoft.EntityFrameworkCore;

namespace RotaMarinho.Application.Services
{
    public class EmbarcacaoService : IEmbarcacaoService
    {
        private readonly AppDbContext _context;

        public EmbarcacaoService(AppDbContext context)
        {
            _context = context;
        }

        // Lista todas as embarcações
        public async Task<IEnumerable<EmbarcacaoDTO>> GetAllEmbarcacoesAsync()
        {
            var embarcacoes = await _context.Embarcacoes.ToListAsync();
            return embarcacoes.Select(e => new EmbarcacaoDTO
            {
                Id = e.Id,
                Nome = e.Nome,
                Matricula = e.Matricula,
                Capacidade = e.Capacidade,
                TipoAlocacao = e.TipoAlocacao,
                PrecoPorHora = e.PrecoPorHora ?? 0m,
                PrecoPorTrabalho = e.PrecoPorTrabalho ?? 0m,
                Status = e.Status,
            });
        }

        // Obter uma embarcação pelo ID
        public async Task<EmbarcacaoDTO> GetEmbarcacaoByIdAsync(int id)
        {
            var embarcacao = await _context.Embarcacoes.FindAsync(id);
            if (embarcacao == null)
            {
                return null;
            }

            return new EmbarcacaoDTO
            {
                Id = embarcacao.Id,
                Nome = embarcacao.Nome,
                Matricula = embarcacao.Matricula,
                Capacidade = embarcacao.Capacidade,
                TipoAlocacao = embarcacao.TipoAlocacao,
                PrecoPorHora = embarcacao.PrecoPorHora ?? 0m,
                PrecoPorTrabalho = embarcacao.PrecoPorTrabalho ?? 0m,
                Status = embarcacao.Status,
            };
        }

        // Obter uma embarcação pela matrícula
        public async Task<EmbarcacaoDTO> GetEmbarcacaoByMatriculaAsync(string matricula)
        {
            var embarcacao = await _context.Embarcacoes
                .FirstOrDefaultAsync(e => e.Matricula == matricula);

            if (embarcacao == null)
            {
                return null;
            }

            return new EmbarcacaoDTO
            {
                Id = embarcacao.Id,
                Nome = embarcacao.Nome,
                Matricula = embarcacao.Matricula,
                Capacidade = embarcacao.Capacidade,
                TipoAlocacao = embarcacao.TipoAlocacao,
                PrecoPorHora = embarcacao.PrecoPorHora ?? 0m,
                PrecoPorTrabalho = embarcacao.PrecoPorTrabalho ?? 0m,
                Status = embarcacao.Status,
            };
        }

        // Adiciona uma nova embarcação
        public async Task AddEmbarcacaoAsync(EmbarcacaoDTO embarcacaoDTO)
        {
            var embarcacao = new Embarcacao
            {
                Nome = embarcacaoDTO.Nome,
                Matricula = embarcacaoDTO.Matricula,
                Capacidade = embarcacaoDTO.Capacidade,
                TipoAlocacao = embarcacaoDTO.TipoAlocacao,
                PrecoPorHora = embarcacaoDTO.PrecoPorHora,
                PrecoPorTrabalho = embarcacaoDTO.PrecoPorTrabalho,
                Status = embarcacaoDTO.Status
            };

            _context.Embarcacoes.Add(embarcacao);
            await _context.SaveChangesAsync();
        }

        // Atualiza uma embarcação existente
        public async Task UpdateEmbarcacaoAsync(int id, EmbarcacaoDTO embarcacaoDTO)
        {
            var embarcacao = await _context.Embarcacoes.FindAsync(id);
            if (embarcacao != null)
            {
                embarcacao.Nome = embarcacaoDTO.Nome;
                embarcacao.Matricula = embarcacaoDTO.Matricula;
                embarcacao.Capacidade = embarcacaoDTO.Capacidade;
                embarcacao.TipoAlocacao = embarcacaoDTO.TipoAlocacao;
                embarcacao.PrecoPorHora = embarcacaoDTO.PrecoPorHora;
                embarcacao.PrecoPorTrabalho = embarcacaoDTO.PrecoPorTrabalho;
                embarcacao.Status = embarcacaoDTO.Status;

                await _context.SaveChangesAsync();
            }
        }

        // Deleta uma embarcação
        public async Task DeleteEmbarcacaoAsync(int id)
        {
            var embarcacao = await _context.Embarcacoes.FindAsync(id);
            if (embarcacao != null)
            {
                _context.Embarcacoes.Remove(embarcacao);
                await _context.SaveChangesAsync();
            }
        }

        // Lista embarcações disponíveis em uma determinada data
        public async Task<IEnumerable<EmbarcacaoDTO>> GetEmbarcacoesDisponiveisAsync(DateTime dataInicio, DateTime dataFim)
        {
            var embarcacoes = await _context.Embarcacoes
                .Where(b => b.Status == "Disponível" &&
                            !_context.Reservas.Any(r => r.EmbarcacaoId == b.Id &&
                            ((r.DataInicio <= dataFim && r.DataFim >= dataInicio))))
                .ToListAsync();

            return embarcacoes.Select(e => new EmbarcacaoDTO
            {
                Id = e.Id,
                Nome = e.Nome,
                Matricula = e.Matricula,
                Capacidade = e.Capacidade,
                TipoAlocacao = e.TipoAlocacao,
                PrecoPorHora = e.PrecoPorHora ?? 0m,
                PrecoPorTrabalho = e.PrecoPorTrabalho ?? 0m,
                Status = e.Status,
            });
        }
    }
}
