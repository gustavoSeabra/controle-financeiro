using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Model;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiroApi.Test.Suport.Mocks;
using ControleFinanceiroDomain.Enum;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace ControleFinanceiroApi.Test.Suport
{
    public class ServiceCollectionsTest
    {
        private readonly ILancamentoApplication lancamentoApplication;
        private readonly Mock<ILancamentoRepository> lancamentoRepositoryMock;

        public ServiceCollectionsTest()
        {
            lancamentoRepositoryMock = new Mock<ILancamentoRepository>();
            lancamentoApplication = new LancamentoApplication(lancamentoRepositoryMock.Object, new LoggerFactory());
        }

        [Fact(DisplayName ="Teste inserir novo lancamento")]
        public async Task Post_InserirLancamento_Sucess()
        {
            // Arrange
            var expected = LancamentoMock.GetFaker(TipoLancamento.Receita);

            lancamentoRepositoryMock.Setup(m => m.InserirLancamento(It.IsAny<Lancamento>()))
                .Returns(Task.FromResult(true));

            // Act
            await lancamentoApplication.InserirLancamento(expected);

            // Assert
            lancamentoRepositoryMock.Verify(x => x.InserirLancamento(It.IsAny<Lancamento>()), Times.Once);
        }

        [Fact(DisplayName = "Teste inserir lista de lancamentos")]
        public async Task Post_InserirLancamentos_Sucess()
        {
            // Arrange
            var expected = LancamentoMock.GetListFaker(4, TipoLancamento.Despesa);

            lancamentoRepositoryMock.Setup(m => m.InserirLancamentos(It.IsAny<List<Lancamento>>()))
                .Returns(Task.FromResult(true));

            // Act
            await lancamentoApplication.InserirLancamentos(expected);

            // Assert
            lancamentoRepositoryMock.Verify(x => x.InserirLancamentos(It.IsAny<List<Lancamento>>()), Times.Once);
        }

        [Fact(DisplayName = "Teste obter lancamento por dia")]
        public async Task Get_ObterLancamentoPorDia_Sucess()
        {
            // Arrange
            var expected = LancamentoMock.GetListFaker(2, TipoLancamento.Despesa);

            lancamentoRepositoryMock.Setup(m => m.ObterLancamentosPorDia(It.IsAny<DateTime>()))
                .Returns(Task.FromResult(expected));

            // Act
            var response = await lancamentoApplication.ObterLancamentosPorDia(DateTime.Now);

            // Assert
            lancamentoRepositoryMock.Verify(x => x.ObterLancamentosPorDia(It.IsAny<DateTime>()), Times.Once);
            response.Count().Should().Be(expected.Count());
        }

        [Fact(DisplayName = "Teste obter todos os lancamento")]
        public async Task Get_ObterTodosLancamentos_Sucess()
        {
            // Arrange
            var expected = LancamentoMock.GetListFaker(8, TipoLancamento.Receita);

            lancamentoRepositoryMock.Setup(m => m.ObterTodosLancamentos())
                .Returns(Task.FromResult(expected));

            // Act
            var response = await lancamentoApplication.ObterTodosLancamentos();

            // Assert
            lancamentoRepositoryMock.Verify(x => x.ObterTodosLancamentos(), Times.Once);
            response.Count().Should().Be(expected.Count());
        }

        [Fact(DisplayName = "Teste obter o fluxo de caixa")]
        public async Task Get_ObterFluxoCaixa_Sucess()
        {
            // Arrange
            var expected = FluxoCaixaMock.GetListFaker(10);

            lancamentoRepositoryMock.Setup(m => m.ObterFluxoCaixa())
                .Returns(Task.FromResult(expected));

            // Act
            var response = await lancamentoApplication.ObterFluxoCaixa();

            // Assert
            lancamentoRepositoryMock.Verify(x => x.ObterFluxoCaixa(), Times.Once);
            response.Count().Should().Be(expected.Count());
        }

        [Fact(DisplayName = "Teste obter os dias com saldo negativo")]
        public async Task Get_ObterDiasSaldoNegativo_Sucess()
        {
            // Arrange
            var expected = FluxoCaixaMock.GetListFaker(10);

            lancamentoRepositoryMock.Setup(m => m.ObterFluxoCaixa())
                .Returns(Task.FromResult(expected));

            // Act
            var response = await lancamentoApplication.ObterDiasSaldoNegativo();

            // Assert
            lancamentoRepositoryMock.Verify(x => x.ObterFluxoCaixa(), Times.Once);
            response.Where(f => f.SaldoFinalDia < 0).Should().HaveCountGreaterThan(0);
        }
    }
}
