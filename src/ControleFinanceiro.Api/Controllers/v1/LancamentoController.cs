using AutoMapper;
using ControleFinanceiro.Api.Dtos.Requests;
using ControleFinanceiro.CrossCutting.Extensions;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiroDomain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleFinanceiro.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentoController : ControllerBase
    {
        private readonly ILancamentoApplication _lancamentoService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LancamentoController(ILancamentoApplication lancamentoService, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _lancamentoService = lancamentoService;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<LancamentoController>();
        }

        /// <summary>
        /// Efetua a busca de todos os lançamentos
        /// </summary>
        /// <response code="200">Lista de todos os lançamentos cadastrados</response>
        /// <response code="400">Os dados não correspondem às regras de validação</response>
        /// <response code="404">Não existem lançamentos cadastrados</response>
        /// <response code="500">Erro não identificado internamente do servidor</response>
        [HttpGet, Route("ObterTodosLancamentos", Name =nameof(ObterTodosLancamentos))]
        public async Task<IActionResult> ObterTodosLancamentos()
        {
            var lancamentos = await _lancamentoService.ObterTodosLancamentos();

            if (lancamentos.Any() is false)
                return NotFound("Não foram encontrados lançamentos");

            return Ok(lancamentos);
        }


        /// <summary>
        /// Efetua a busca de lançamentos de um dia específico
        /// </summary>
        /// <param name="dataLancamento">Dia para filtrar os lançamentos</param>
        /// <response code="200">Lista de lançamentos filtrada</response>
        /// <response code="400">Os dados não correspondem às regras de validação</response>
        /// <response code="404">Não existem lançamentos para este dia</response>
        /// <response code="500">Erro não identificado internamente do servidor</response>
        [HttpGet, Route("ObterLancamentosPorDia", Name = nameof(ObterLancamentosPorDia))]
        public async Task<IActionResult> ObterLancamentosPorDia(DateTime dataLancamento)
        {
            var lancamentos = await _lancamentoService.ObterLancamentosPorDia(dataLancamento);

            if (lancamentos.Any() is false)
                return NotFound("Não foram encontrados lançamentos para a data informada");

            return Ok(lancamentos);
        }

        /// <summary>
        /// Efetua a inclusão de lançamento
        /// </summary>
        /// <param name="LancamentoDto">Informações do lançamento financeiro</param>
        /// <response code="200">O lançamento foi salvo com sucesso</response>
        /// <response code="400">Os dados não correspondem às regras de validação</response>
        /// <response code="500">Erro não identificado internamente do servidor</response>
        [HttpPost, Route("InserirLancamento", Name = nameof(InserirLancamento))]
        public async Task<IActionResult> InserirLancamento([FromBody] LancamentoDto request)
        {
            _logger.LogInformation("Testando o logg");
            var lancamento = _mapper.Map<Lancamento>(request);

            await _lancamentoService.InserirLancamento(lancamento);

            return NoContent();
        }


        /// <summary>
        /// Efetua a inclusão de vários lançamentos
        /// </summary>
        /// <param name="LancamentoDto">Informações do lançamento financeiro</param>
        /// <response code="204">O lançamento foi salvo com sucesso</response>
        /// <response code="400">Os dados não correspondem às regras de validação</response>
        /// <response code="500">Erro não identificado internamente do servidor</response>
        [HttpPost, Route("InserirLancamentos", Name = nameof(InserirLancamentos))]
        public async Task<IActionResult> InserirLancamentos([FromBody] List<LancamentoDto> request)
        {
            var lancamentos = _mapper.Map<List<Lancamento>>(request);
            
            await _lancamentoService.InserirLancamentos(lancamentos);

            return NoContent();
        }
    }
}
