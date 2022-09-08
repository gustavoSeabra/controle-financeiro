using Bogus;
using ControleFinanceiro.Api.Dtos.Requests;
using ControleFinanceiroDomain.Enum;

namespace ControleFinanceiroApi.Test.Suport.Mocks
{
    public static class LancamentoDtoMock
    {
        public static LancamentoDto GetFaker(TipoLancamento tipo)
        {
            var faker = GetListFaker(1, tipo).FirstOrDefault();
            if (faker == null)
                return new LancamentoDto();

            return faker;
        }

        public static List<LancamentoDto> GetListFaker(int quantity, TipoLancamento tipo)
        {
            var random = new Random();
            var fakeGenerator = new Faker<LancamentoDto>()
                .CustomInstantiator(x => new LancamentoDto
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
