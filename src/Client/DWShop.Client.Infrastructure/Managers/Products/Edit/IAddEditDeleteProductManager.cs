using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Application.Features.Catalog.Commands.Delete;
using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Products.Edit
{
    public interface IAddEditDeleteProductManager:IManager
    {
        Task<IResult> DeleteProduct(DeleteCatalogCommand command);
        Task<IResult> EditProduct(UpdateCatalogCommand command);
        Task<IResult> AddProduct(CreateCatalogCommand command);
    }
}