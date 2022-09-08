using AutoMapper;
using ControleFinanceiro.Api;

namespace ControleFinanceiroApi.Test.Suport.Mapper
{
    public class MapperFixture
    {
        public IMapper Mapper { get; }

        public MapperFixture()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new WebApiAutoMapperProfile());
            });

            Mapper = config.CreateMapper();
        }
    }
}
