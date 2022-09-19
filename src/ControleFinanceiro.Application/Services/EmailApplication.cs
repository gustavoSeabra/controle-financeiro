using ControleFinanceiro.Domain.Model;
using ControleFinanceiro.Domain.Services;
using MimeKit;
using MimeKit.Text;
using System.Diagnostics.CodeAnalysis;

namespace ControleFinanceiro.Application.Services
{
    [ExcludeFromCodeCoverage]
    public class EmailApplication : IEmailService
    {
        private readonly ISmtpClientGenerator _smtClient;
        public EmailApplication(ISmtpClientGenerator smtClient)
        {
            _smtClient = smtClient;
        }

        public async Task SendAsync(Email mail)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mail.De));
            email.To.Add(MailboxAddress.Parse(mail.Para));
            email.Subject = mail.Assunto;
            email.Body = new TextPart(TextFormat.Html) { Text = mail.Mensagem };

            // send email
            await _smtClient.GenerateClient().SendAsync(email);
        }
    }
}