using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Pedidos.Commands.Delete
{
    public class DeletePedidosCommand : IRequest<IResult>
    {
        public int Id { get; set; }
    }
}
