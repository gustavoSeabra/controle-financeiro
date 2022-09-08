using ControleFinanceiro.Domain.Model;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Domain.Services;
using Microsoft.Extensions.Logging;

namespace ControleFinanceiro.Application.Services
{
    public class LancamentoApplication : ILancamentoApplication
    {
        private readonly ILancamentoRepository _repository;
        private readonly ILogger _logger;

        public LancamentoApplication(ILancamentoRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<LancamentoApplication>();
        }

        public async Task InserirLancamento(Lancamento lancamento)
        {
            _logger.LogInformation("Realizando chamada ao repositório para salvar um lançamento");
            await _repository.InserirLancamento(lancamento);
            _logger.LogInformation("Chamada ao serviço InserirLancamento concluida com sucesso.");
        }

        public async Task InserirLancamentos(List<Lancamento> lancamentos)
        {
            _logger.LogInformation("Realizando chamada ao repositório para salvar uma lista de lançamento");
            await _repository.InserirLancamentos(lancamentos);
            _logger.LogInformation("Chamada ao serviço InserirLancamentos concluida com sucesso.");
        }

        public async Task<List<FluxoCaixa>> ObterDiasSaldoNegativo()
        {
            _logger.LogInformation("Realizando chamada ao repositório para obter o fluxo de caixa");
            var fluxoCaixa = await _repository.ObterFluxoCaixa();
            _logger.LogInformation("Chamada ao serviço ObterFluxoCaixa concluida com sucesso.");

            return await Task.FromResult(fluxoCaixa.Where(f => f.SaldoFinalDia < 0).ToList());

        }

        public async Task<List<FluxoCaixa>> ObterFluxoCaixa()
        {
            _logger.LogInformation("Realizando chamada ao repositório para obter o fluxo de caixa");
            return await _repository.ObterFluxoCaixa();
        }

        public async Task<List<Lancamento>> ObterLancamentosPorDia(DateTime dataLancamento)
        {
            _logger.LogInformation("Realizando chamada ao repositório para obter o lançamentos por dia");
            return await _repository.ObterLancamentosPorDia(dataLancamento);
        }

        public async Task<List<Lancamento>> ObterTodosLancamentos()
        {
            _logger.LogInformation("Realizando chamada ao repositório para obter todos os lançamentos");
            return await _repository.ObterTodosLancamentos();
        }
    }
}
