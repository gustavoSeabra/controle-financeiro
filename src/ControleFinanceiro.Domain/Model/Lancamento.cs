using ControleFinanceiroDomain.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiroDomain.Models
{
    public class Lancamento
    {
        [Key]
        public Guid ID { get; private set; }
        [Required]
        public TipoLancamento Tipo { get; set; }
        [Required]
        public DateTime Data { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Precision(18, 2)]
        public decimal Valor { get; set; }

        public Lancamento()
        {
            if (ID == Guid.Empty)
                ID = Guid.NewGuid();
        }
    }
}
