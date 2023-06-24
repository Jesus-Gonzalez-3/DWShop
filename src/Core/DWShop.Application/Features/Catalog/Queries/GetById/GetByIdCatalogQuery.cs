using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Catalog.Queries.GetById
{
    public class GetByIdCatalogQuery: IRequest<IResult>
    {
        public int Id { get; set; }
    }
}
