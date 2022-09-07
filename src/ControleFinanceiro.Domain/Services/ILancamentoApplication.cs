using ControleFinanceiroDomain.Models;

namespace ControleFinanceiro.Domain.Services
{
    public interface ILancamentoApplication
    {
        Task InserirLancamento(Lancamento lancamento);
        Task InserirLancamentos(List<Lancamento> lancamentos);
        Task<List<Lancamento>> ObterLancamentosPorDia(DateTime dataLancamento);
        Task<List<Lancamento>> ObterTodosLancamentos();
    }
}
