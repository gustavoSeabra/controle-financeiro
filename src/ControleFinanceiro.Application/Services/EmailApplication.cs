using ControleFinanceiro.Domain.Model;
using ControleFinanceiro.Domain.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace ControleFinanceiro.Application.Services
{
    public class EmailApplication : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailApplication(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(Email mail)
        {
            var smtpHost = _configuration.GetRequiredSection("EmailServer:SmtpHost").Value;
            var smtpPort = Convert.ToInt32(_configuration.GetRequiredSection("EmailServer:SmtpPort").Value);
            var smtpUser = _configuration.GetRequiredSection("EmailServer:SmtpUser").Value;
            var smtpPass = _configuration.GetRequiredSection("EmailServer:SmtpPass").Value;

            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mail.De));
            email.To.Add(MailboxAddress.Parse(mail.Para));
            email.Subject = mail.Assunto;
            email.Body = new TextPart(TextFormat.Html) { Text = mail.Mensagem };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(smtpUser, smtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
