using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Application.Interfaces.Repositories;
using DWShop.Domain.Entities;
using DWShop.Shared.Wrapper;
using MediatR;
using CatalogEntity = DWShop.Domain.Entities.Catalog;

namespace DWShop.Application.Features.Catalog.Queries.GetById
{
    public class GetByIdCatalogQueryHandler : IRequestHandler<GetByIdCatalogQuery, IResult>
    {
        private readonly IRepositoryAsync<CatalogEntity, int> repositoryAsync;

        public GetByIdCatalogQueryHandler(IRepositoryAsync<CatalogEntity, int> repositoryAsync)
        {
            this.repositoryAsync = repositoryAsync;
        }

        public async Task<IResult> Handle(GetByIdCatalogQuery request, CancellationToken cancellationToken)
        {
            CatalogEntity? entity = await repositoryAsync.GetByIdAsync(request.Id);
            if (entity is null)
                return await Result.FailAsync("Producto no encontrado");
            return await Result<CatalogEntity>.SuccessAsync(entity, "");
        }
    }
}
