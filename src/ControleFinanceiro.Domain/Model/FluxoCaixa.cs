using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Domain.Model
{
    [Keyless]
    public class FluxoCaixa
    {
        public DateTime Data { get; set; }

        [Precision(18, 2)]
        public decimal SaldoFinalDia { get; set; }
    }
}
