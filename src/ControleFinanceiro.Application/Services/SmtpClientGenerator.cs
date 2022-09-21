using ControleFinanceiro.Domain.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace ControleFinanceiro.Application.Services
{
    [ExcludeFromCodeCoverage]
    public class SmtpClientGenerator : ISmtpClientGenerator
    {
        private readonly IConfiguration _configuration;

        public SmtpClientGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ISmtpClient GenerateClient()
        {
            var smtpHost = _configuration.GetRequiredSection("EmailServer:SmtpHost").Value;
            var smtpPort = Convert.ToInt32(_configuration.GetRequiredSection("EmailServer:SmtpPort").Value);
            var smtpUser = _configuration.GetRequiredSection("EmailServer:SmtpUser").Value;
            var smtpPass = _configuration.GetRequiredSection("EmailServer:SmtpPass").Value;

            var smtp = new SmtpClient();
            smtp.Connect(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(smtpUser, smtpPass);
            
            return smtp;
        }
    }
}
