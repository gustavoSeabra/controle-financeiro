using AutoMapper;
using ControleFinanceiro.Api.Dtos.Requests;
using ControleFinanceiro.Domain.Model;

namespace ControleFinanceiro.Api
{
    public class WebApiAutoMapperProfile : Profile
    {
        public WebApiAutoMapperProfile()
        {
            CreateMap<LancamentoDto, Lancamento>();
            CreateMap<Lancamento, LancamentoDto>();
        }
    }
}
