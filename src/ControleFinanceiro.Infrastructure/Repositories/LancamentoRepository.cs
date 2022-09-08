using ControleFinanceiro.Domain.Model;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiroInfrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class LancamentoRepository : ILancamentoRepository
    {
        private readonly SqlServerContext _context;

        public LancamentoRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task InserirLancamento(Lancamento lancamento)
        {
            await _context.Lancamentos.AddAsync(lancamento);
            await _context.SaveChangesAsync();
        }

        public async Task InserirLancamentos(List<Lancamento> lancamentos)
        {
            await _context.Lancamentos.AddRangeAsync(lancamentos);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FluxoCaixa>> ObterFluxoCaixa()
        {
            return await _context.FluxoCaixa.FromSqlRaw("exec ObterFluxoCaixa").ToListAsync();
        }

        public async Task<List<Lancamento>> ObterLancamentosPorDia(DateTime dataLancamento)
        { 
            return await _context.Lancamentos.Where(l => l.Data.Equals(dataLancamento)).ToListAsync();
        }

        public Task<List<Lancamento>> ObterTodosLancamentos()
        {
            return _context.Lancamentos.ToListAsync();
        }
    }
}
