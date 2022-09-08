using ControleFinanceiro.Domain.Model;

namespace ControleFinanceiro.Domain.Services
{
    public interface IEmailService
    {
        void Send(Email email);
    }
}
