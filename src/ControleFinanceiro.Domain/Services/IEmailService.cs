using ControleFinanceiro.Domain.Model;

namespace ControleFinanceiro.Domain.Services
{
    public interface IEmailService
    {
        Task SendAsync(Email mail);
    }
}
