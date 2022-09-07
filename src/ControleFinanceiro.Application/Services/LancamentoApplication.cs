using ControleFinanceiro.Domain.Services;
using ControleFinanceiroDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Application.Services
{
    public class LancamentoApplication : ILancamentoApplication
    {
        public Task InserirLancamento(Lancamento lancamento)
        {
            throw new NotImplementedException();
        }

        public Task InserirLancamentos(List<Lancamento> lancamentos)
        {
            throw new NotImplementedException();
        }

        public Task<List<Lancamento>> ObterLancamentosPorDia(DateTime dataLancamento)
        {
            throw new NotImplementedException();
        }

        public Task<List<Lancamento>> ObterTodosLancamentos()
        {
            throw new NotImplementedException();
        }
    }
}
