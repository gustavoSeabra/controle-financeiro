using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Model;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiroApi.Test.Suport.Mocks;
using FluentAssertions;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Moq;
using System.Net.Mail;

namespace ControleFinanceiroApi.Test.Unit
{
    public class EmailApplicationTest
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<ISmtpClientGenerator> _SmtpClientGenerator;
        private readonly Mock<ISmtpClient> _mockSmtpClient;

        public EmailApplicationTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _SmtpClientGenerator = _mockRepository.Create<ISmtpClientGenerator>();
            _mockSmtpClient = _mockRepository.Create<ISmtpClient>();
        }

        [Fact(DisplayName = "Teste enviar e-mail")]
        public async void Enviar_Send_Sucess()
        {
            // Arrange
            var mail = EmailMock.GetFaker();
            var mime = GeteMimeMessage(EmailMock.GetFaker());

            _mockSmtpClient.Setup(m => m.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>()))
                .Returns(Task.FromResult(string.Empty));

            _SmtpClientGenerator.Setup(m => m.GenerateClient())
                .Returns(_mockSmtpClient.Object);

            var servico = new EmailApplication(_SmtpClientGenerator.Object);

            // Act
            var result = servico.SendAsync(mail);
            await result;

            // Assert
            result.IsCompletedSuccessfully.Should().BeTrue();
            _mockRepository.VerifyAll();
        }

        #region Helpers

        private static MimeMessage GeteMimeMessage(Email mail)
        {
            var mime = new MimeMessage();

            mime.From.Add(MailboxAddress.Parse(mail.De));
            mime.To.Add(MailboxAddress.Parse(mail.Para));
            mime.Subject = mail.Assunto;
            mime.Body = new TextPart(TextFormat.Html) { Text = mail.Mensagem };

            return mime;
        }

        #endregion
    }
}
