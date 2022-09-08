using Bogus;
using ControleFinanceiro.Domain.Model;

namespace ControleFinanceiroApi.Test.Suport.Mocks
{
    public static class FluxoCaixaMock
    {
        public static FluxoCaixa GetFaker()
        {
            var faker = GetListFaker(1).FirstOrDefault();
            if (faker == null)
                return new FluxoCaixa();

            return faker;
        }

        public static List<FluxoCaixa> GetListFaker(int quantity)
        {
            var random = new Random();
            var fakeGenerator = new Faker<FluxoCaixa>()
                .CustomInstantiator(x => new FluxoCaixa
                {
                    Data = DateTime.Now,
                    SaldoFinalDia = random.Next(-100, 100)
                });

            var fakes = fakeGenerator.Generate(quantity);

            return fakes;
        }
    }
}
