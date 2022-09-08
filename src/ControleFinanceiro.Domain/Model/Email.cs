namespace ControleFinanceiro.Domain.Model
{
    public class Email
    {
        public string Para { get; set; } = string.Empty;
        public string Assunto { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public string De { get; set; } = string.Empty;
    }
}
