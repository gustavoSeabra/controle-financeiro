using AutoMapper;
using ControleFinanceiro.Api.Controllers.v1;
using ControleFinanceiro.Api.Dtos.Requests;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Model;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiroApi.Test.Suport.Mapper;
using ControleFinanceiroApi.Test.Suport.Mocks;
using ControleFinanceiroDomain.Enum;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiroApi.Test.Controller
{
    public class LancamentoControllerTest
    {
        private readonly Mock<ILancamentoApplication> _lancamentoServiceMock;
        private readonly IMapper _mapper;
        private readonly LancamentoController controllerTest;

        public LancamentoControllerTest()
        {
            _lancamentoServiceMock = new Mock<ILancamentoApplication>();
            MapperFixture mapperFixture = new MapperFixture();
            _mapper = mapperFixture.Mapper;
            controllerTest = new LancamentoController(_lancamentoServiceMock.Object, _mapper);
        }

        [Fact(DisplayName = "Teste inserir novo lancamento")]
        public async Task Post_InserirLancamento_Sucess()
        {
            // Arrange
            var expected = LancamentoDtoMock.GetFaker(TipoLancamento.Receita);

            _lancamentoServiceMock.Setup(m => m.InserirLancamento(It.IsAny<Lancamento>()))
                .Returns(Task.FromResult(true));

            // Act
            await controllerTest.InserirLancamento(expected);

            // Assert
            _lancamentoServiceMock.Verify(x => x.InserirLancamento(It.IsAny<Lancamento>()), Times.Once);
        }

        [Fact(DisplayName = "Teste inserir lista de lancamentos")]
        public async Task Post_InserirLancamentos_Sucess()
        {
            // Arrange
            var expected = LancamentoDtoMock.GetListFaker(4, TipoLancamento.Despesa);

            _lancamentoServiceMock.Setup(m => m.InserirLancamentos(It.IsAny<List<Lancamento>>()))
                .Returns(Task.FromResult(true));

            // Act
            await controllerTest.InserirLancamentos(expected);

            // Assert
            _lancamentoServiceMock.Verify(x => x.InserirLancamentos(It.IsAny<List<Lancamento>>()), Times.Once);
        }

        [Fact(DisplayName = "Teste obter lancamento por dia")]
        public async Task Get_ObterLancamentoPorDia_Sucess()
        {
            // Arrange
            var expected = LancamentoMock.GetListFaker(2, TipoLancamento.Despesa);

            _lancamentoServiceMock.Setup(m => m.ObterLancamentosPorDia(It.IsAny<DateTime>()))
                .Returns(Task.FromResult(expected));

            // Act
            var response = await controllerTest.ObterLancamentosPorDia(DateTime.Now);

            // Assert
            var okObjectResult = response as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var model = okObjectResult.Value as List<Lancamento>;
            model.Should().NotBeNull();
            model.Count.Should().BeGreaterThanOrEqualTo(2);

            _lancamentoServiceMock.Verify(x => x.ObterLancamentosPorDia(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact(DisplayName = "Teste obter lancamento por dia, lançamentos não encontrado")]
        public async Task Get_ObterLancamentoPorDia_NotFound()
        {
            // Arrange
            _lancamentoServiceMock.Setup(m => m.ObterLancamentosPorDia(It.IsAny<DateTime>()))
                .Returns(Task.FromResult(new List<Lancamento>()));

            // Act
            var response = await controllerTest.ObterLancamentosPorDia(DateTime.Now);

            // Assert
            var okObjectResult = response as NotFoundObjectResult;
            okObjectResult.Should().NotBeNull();

            _lancamentoServiceMock.Verify(x => x.ObterLancamentosPorDia(It.IsAny<DateTime>()), Times.Once);
        }

        
        [Fact(DisplayName = "Teste obter todos os lancamento")]
        public async Task Get_ObterTodosLancamentos_Sucess()
        {
            // Arrange
            var expected = LancamentoMock.GetListFaker(8, TipoLancamento.Receita);

            _lancamentoServiceMock.Setup(m => m.ObterTodosLancamentos())
                .Returns(Task.FromResult(expected));

            // Act
            var response = await controllerTest.ObterTodosLancamentos();

            // Assert
            var okObjectResult = response as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var model = okObjectResult.Value as List<Lancamento>;
            model.Should().NotBeNull();
            model.Count.Should().BeGreaterThanOrEqualTo(8);
            _lancamentoServiceMock.Verify(x => x.ObterTodosLancamentos(), Times.Once);
        }

        [Fact(DisplayName = "Teste obter todos os lancamento, lançamentos não encontrado")]
        public async Task Get_ObterTodosLancamentos_NaoEncontrado()
        {
            // Arrange
            _lancamentoServiceMock.Setup(m => m.ObterTodosLancamentos())
                .Returns(Task.FromResult(new List<Lancamento>()));

            // Act
            var response = await controllerTest.ObterTodosLancamentos();

            // Assert
            var okObjectResult = response as NotFoundObjectResult;
            okObjectResult.Should().NotBeNull();
            _lancamentoServiceMock.Verify(x => x.ObterTodosLancamentos(), Times.Once);
        }

        [Fact(DisplayName = "Teste obter o fluxo de caixa")]
        public async Task Get_ObterFluxoCaixa_Sucess()
        {
            // Arrange
            var expected = FluxoCaixaMock.GetListFaker(10);

            _lancamentoServiceMock.Setup(m => m.ObterFluxoCaixa())
                .Returns(Task.FromResult(expected));

            // Act
            var response = await controllerTest.ObterFluxoDeCaixa();

            // Assert
            var okObjectResult = response as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var model = okObjectResult.Value as List<FluxoCaixa>;
            model.Should().NotBeNull();
            model.Count.Should().BeGreaterThanOrEqualTo(10);
            _lancamentoServiceMock.Verify(x => x.ObterFluxoCaixa(), Times.Once);
        }

        [Fact(DisplayName = "Teste obter o fluxo de caixa, fluxo não encontrado")]
        public async Task Get_ObterFluxoCaixa_NaoEncontrado()
        {
            // Arrange
            _lancamentoServiceMock.Setup(m => m.ObterFluxoCaixa())
                .Returns(Task.FromResult(new List<FluxoCaixa>()));

            // Act
            var response = await controllerTest.ObterFluxoDeCaixa();

            // Assert
            var okObjectResult = response as NotFoundObjectResult;
            okObjectResult.Should().NotBeNull();
            _lancamentoServiceMock.Verify(x => x.ObterFluxoCaixa(), Times.Once);
        }


        [Fact(DisplayName = "Teste obter os dias com saldo negativo")]
        public async Task Get_ObterDiasSaldoNegativo_Sucess()
        {
            // Arrange
            var expected = FluxoCaixaMock.GetListFaker(10);

            _lancamentoServiceMock.Setup(m => m.ObterDiasSaldoNegativo())
                .Returns(Task.FromResult(expected));

            // Act
            var response = await controllerTest.ObterDiasSaldoNegativo();

            // Assert
            var okObjectResult = response as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var model = okObjectResult.Value as List<FluxoCaixa>;
            model.Should().NotBeNull();
            model.Count.Should().BeGreaterThanOrEqualTo(1);
            _lancamentoServiceMock.Verify(x => x.ObterDiasSaldoNegativo(), Times.Once);
        }

        [Fact(DisplayName = "Teste obter os dias com saldo negativo, não encontrado")]
        public async Task Get_ObterDiasSaldoNegativo_NaoEncontrato()
        {
            // Arrange
            _lancamentoServiceMock.Setup(m => m.ObterDiasSaldoNegativo())
                .Returns(Task.FromResult(new List<FluxoCaixa>()));

            // Act
            var response = await controllerTest.ObterDiasSaldoNegativo();

            // Assert
            var okObjectResult = response as NotFoundObjectResult;
            okObjectResult.Should().NotBeNull();
            _lancamentoServiceMock.Verify(x => x.ObterDiasSaldoNegativo(), Times.Once);
        }
    }
}
