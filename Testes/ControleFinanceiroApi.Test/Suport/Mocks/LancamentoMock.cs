using Bogus;
using ControleFinanceiro.Domain.Model;
using ControleFinanceiroDomain.Enum;

namespace ControleFinanceiroApi.Test.Suport.Mocks
{
    public static class LancamentoMock
    {
        public static Lancamento GetFaker(TipoLancamento tipo)
        {
            var faker = GetListFaker(1, tipo).FirstOrDefault();
            if (faker == null)
                return new Lancamento();

            return faker;
        }

        public static List<Lancamento> GetListFaker(int quantity, TipoLancamento tipo)
        {
            var fakeGenerator = new Faker<Lancamento>()
                .CustomInstantiator(x => new Lancamento
                {
                    Data = DateTime.Now,
                    Descricao = $"{tipo.ToString()} teste",
                    Tipo = tipo,
                    Valor = 100
                });

            var fakes = fakeGenerator.Generate(quantity);

            return fakes;
        }
    }
}
