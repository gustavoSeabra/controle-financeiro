using ControleFinanceiroDomain.Models;
using Microsoft.EntityFrameworkCore;


namespace ControleFinanceiroInfrastructure.Contexts
{
    public class SqlServerContext : DbContext
    {
        public DbSet<Lancamento> Lancamentos { get; set; }

        public SqlServerContext(DbContextOptions<SqlServerContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lancamento>().ToTable("Lancamento");
        }
    }
}
