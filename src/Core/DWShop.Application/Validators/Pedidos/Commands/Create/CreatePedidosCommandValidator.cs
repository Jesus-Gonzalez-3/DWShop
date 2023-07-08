using DWShop.Application.Features.Pedidos.Commands.Create;
using FluentValidation;

namespace DWShop.Application.Validators.Pedidos.Commands.Create
{
    public class CreatePedidosCommandValidator : AbstractValidator<CreatePedidosCommand>
    {
        public CreatePedidosCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Debe completar el nombre del usuario")
                .NotNull();

            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha no ha sido completada")
                .NotNull();

            RuleFor(x => x.TotalPrice)
               .GreaterThan(0).WithMessage("El total no puede ser 0 compruebe")
               .NotEmpty().WithMessage("La fecha no ha sido completada")
               .NotNull();
        }
    }
}
