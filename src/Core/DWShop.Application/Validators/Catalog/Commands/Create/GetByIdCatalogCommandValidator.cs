using DWShop.Application.Features.Catalog.Queries.GetById;
using FluentValidation;

namespace DWShop.Application.Validators.Catalog.Commands.Create
{
    public class GetByIdCatalogCommandValidator: AbstractValidator<GetByIdCatalogQuery>
    {
        public GetByIdCatalogCommandValidator()
        {
            RuleFor(x=> x.Id)
                .GreaterThan(0).WithMessage("El Id del producto no puede ser menor o igual a 0")
                .NotEmpty()
                .NotNull();
        }
    }
}
