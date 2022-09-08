using ControleFinanceiro.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiroInfrastructure.Contexts
{
    public class SqlServerContext : DbContext
    {
        public DbSet<Lancamento> Lancamentos { get; set; }

        [NotMapped]
        public DbSet<FluxoCaixa> FluxoCaixa { get; set; }

        public SqlServerContext(DbContextOptions<SqlServerContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lancamento>().ToTable("Lancamento");
        }
    }
}
