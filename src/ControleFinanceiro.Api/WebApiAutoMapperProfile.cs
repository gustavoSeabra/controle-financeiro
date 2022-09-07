using AutoMapper;
using ControleFinanceiro.Api.Dtos.Requests;
using ControleFinanceiroDomain.Models;

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
