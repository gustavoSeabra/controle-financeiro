using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiroDomain.Models;
using ControleFinanceiroInfrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Infrastructure.Repositories
{
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
