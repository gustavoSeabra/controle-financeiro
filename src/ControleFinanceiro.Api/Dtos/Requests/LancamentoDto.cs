using ControleFinanceiroDomain.Enum;

namespace ControleFinanceiro.Api.Dtos.Requests
{
    public class LancamentoDto
    {
        public TipoLancamento Tipo { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}
