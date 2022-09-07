using ControleFinanceiro.Api.Dtos.Requests;
using FluentValidation;

namespace ControleFinanceiro.Api.Validators
{
    public class LancamentoDtoValidator : AbstractValidator<LancamentoDto>
    {
        public LancamentoDtoValidator()
        {
            RuleFor(r => r.Tipo)
                .NotEmpty()
                    .WithMessage("[Tipo] precisa ser informado");

            RuleFor(r => r.Descricao)
                .NotEmpty().WithMessage("[Descricao] precisa ser informado");

            RuleFor(r => r.Valor)
                .NotEmpty().WithMessage("[Valor] precisa ser informado");
        }
    }
}
