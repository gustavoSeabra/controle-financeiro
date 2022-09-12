using ControleFinanceiro.Domain.Model;
using ControleFinanceiro.Domain.Services;
using System.Net.Mail;

namespace ControleFinanceiro.Application.Services
{
    public class EmailApplication : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        public EmailApplication(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendAsync(Email mail)
        {
            // send email
            await _smtpClient.SendMailAsync(new MailMessage(mail.De, mail.Para, mail.Assunto, mail.Mensagem));
            _smtpClient.Dispose();
        }
    }
}
