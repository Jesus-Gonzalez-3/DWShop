using DWShop.Application.Interfaces.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;
using CatalogEntity = DWShop.Domain.Entities.Catalog;


namespace DWShop.Application.Features.Catalog.Commands.Delete
{
    public class DeleteCatalogCommandHandler : IRequestHandler<DeleteCatalogCommand, IResult>
    {
        private readonly IRepositoryAsync<CatalogEntity, int> repositoryAsync;

        public DeleteCatalogCommandHandler(IRepositoryAsync<CatalogEntity, int> repositoryAsync)
        {
            this.repositoryAsync = repositoryAsync;
        }
        public async Task<IResult> Handle(DeleteCatalogCommand request, CancellationToken cancellationToken)
        {
            var catalog = await repositoryAsync.GetByIdAsync(request.Id);
            if (catalog is null)
                return await Result.FailAsync("Producto no encontrado");
            await repositoryAsync.DeleteAsync(catalog);
            await repositoryAsync.SaveChangesAsync();
            return await Result.SuccessAsync();
        }
    }
}
