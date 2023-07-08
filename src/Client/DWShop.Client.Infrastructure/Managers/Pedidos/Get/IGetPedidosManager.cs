using DWShop.Application.Responses.Pedidos;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Pedidos.Get
{
    public interface IGetPedidosManager:IManager
    {
        Task<IResult<IEnumerable<PedidosResponse>>> GetAllPedidos();
    }
}