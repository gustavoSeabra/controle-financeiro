using MailKit.Net.Smtp;

namespace ControleFinanceiro.Domain.Services
{
    public interface ISmtpClientGenerator
    {
        ISmtpClient GenerateClient();
    }
}
