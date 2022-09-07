using ControleFinanceiroDomain.Models;

namespace ControleFinanceiro.Domain.Repositories
{
    public interface ILancamentoRepository
    {
        Task InserirLancamento(Lancamento lancamento);
        Task InserirLancamentos(List<Lancamento> lancamentos);
        Task<List<Lancamento>> ObterLancamentosPorDia(DateTime dataLancamento);
        Task<List<Lancamento>> ObterTodosLancamentos();
    }
}
