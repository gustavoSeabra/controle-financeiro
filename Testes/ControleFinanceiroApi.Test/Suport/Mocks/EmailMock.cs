using Bogus;
using ControleFinanceiro.Domain.Model;

namespace ControleFinanceiroApi.Test.Suport.Mocks
{
    public static class EmailMock
    {
        public static Email GetFaker()
        {
            var faker = GetListFaker(1).FirstOrDefault();
            if (faker == null)
                return new Email();

            return faker;
        }

        public static List<Email> GetListFaker(int quantity)
        {
            var random = new Random();
            var fakeGenerator = new Faker<Email>()
                .CustomInstantiator(x => new Email
                {
                    Para = "contoso@teste.com",
                    Assunto = $"Assunto Faker {random.Next(0, 100)}",
                    Mensagem = $"Mensagem Faker {random.Next(0, 100)}",
                    De = "controleFinanceiro@teste.com"
                });

            var fakes = fakeGenerator.Generate(quantity);

            return fakes;
        }
    }
}
