using DWShop.Application.Responses.Pedidos;
using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Pedidos.Queries
{
    public class GetPedidosQuery: IRequest<IResult<IEnumerable<PedidosResponse>>>
    {
    }
}
