using ControleFinanceiro.Domain.Model;

namespace ControleFinanceiro.Domain.Services
{
    public interface ILancamentoApplication
    {
        Task InserirLancamento(Lancamento lancamento);
        Task InserirLancamentos(List<Lancamento> lancamentos);
        Task<List<Lancamento>> ObterLancamentosPorDia(DateTime dataLancamento);
        Task<List<Lancamento>> ObterTodosLancamentos();
        Task<List<FluxoCaixa>> ObterFluxoCaixa();
        Task<List<FluxoCaixa>> ObterDiasSaldoNegativo();
    }
}
