using Microsoft.EntityFrameworkCore;
using RotaMarinho.Models;

namespace RotaMarinho.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        // API interna que dá suporte à infraestrutura do Entity Framework Core
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 

        // DbSet representa a coleção de todas as entidades no contexto ou que podem ser consultadas no DB
        public DbSet<Embarcacao> Embarcacoes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        
        // Método para configurar o modelo (precisão de decimal, chaves, etc.)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurando a precisão para os valores decimais da Embarcacao
            modelBuilder.Entity<Embarcacao>()
                .Property(e => e.PrecoPorHora)
                .HasPrecision(7, 2);  // 7 dígitos totais, 2 casas decimais

            modelBuilder.Entity<Embarcacao>()
                .Property(e => e.PrecoPorTrabalho)
                .HasPrecision(8, 2);  // 8 dígitos totais, 2 casas decimais

            // Configurando a precisão para o valor decimal da Reserva
            modelBuilder.Entity<Reserva>()
                .Property(r => r.Valor)
                .HasPrecision(8, 2);  // 8 dígitos totais, 2 casas decimais
        }
    }
}