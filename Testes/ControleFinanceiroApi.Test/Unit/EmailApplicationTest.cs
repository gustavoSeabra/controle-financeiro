using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Model;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiroApi.Test.Suport.Mocks;
using FluentAssertions;
using Moq;
using System.Net.Mail;

namespace ControleFinanceiroApi.Test.Unit
{
    public class EmailApplicationTest
    {
        private readonly Mock<IEmailService> smtpClientMock;

        public EmailApplicationTest()
        {
            smtpClientMock = new Mock<IEmailService>();
        }

        [Fact(DisplayName = "Teste enviar e-mail")]
        public async Task Enviar_Send_SucessAsync()
        {
            // Arrange
            var mail = EmailMock.GetFaker();

            smtpClientMock.Setup(m => m.SendAsync(It.IsAny<Email>()))
                .Returns(Task.FromResult(true));

            // Act
            await smtpClientMock.Object.SendAsync(mail);

            // Assert
            smtpClientMock.Verify(x => x.SendAsync(It.IsAny<Email>()), Times.Once);
        }
    }
}
