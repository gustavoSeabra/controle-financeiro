using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiroDomain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Application.Services
{
    public class LancamentoApplication : ILancamentoApplication
    {
        private readonly ILancamentoRepository _repository;
        private readonly ILogger _logger;

        public LancamentoApplication(ILancamentoRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<LancamentoApplication>(); ;
        }

        public async Task InserirLancamento(Lancamento lancamento)
        {
            await _repository.InserirLancamento(lancamento);
        }

        public async Task InserirLancamentos(List<Lancamento> lancamentos)
        {
            await _repository.InserirLancamentos(lancamentos);
        }

        public async Task<List<Lancamento>> ObterLancamentosPorDia(DateTime dataLancamento)
        {
            return await _repository.ObterLancamentosPorDia(dataLancamento);
        }

        public async Task<List<Lancamento>> ObterTodosLancamentos()
        {
            return await _repository.ObterTodosLancamentos();
        }
    }
}
