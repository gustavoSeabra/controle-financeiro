using ControleFinanceiroDomain.Enum;
using System;

namespace ControleFinanceiroDomain.Models
{
    public class Lancamento
    {
        public Guid ID { get; private set; }
        public TipoLancamento Tipo { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}
