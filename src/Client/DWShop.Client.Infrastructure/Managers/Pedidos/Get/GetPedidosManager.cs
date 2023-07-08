using DWShop.Application.Responses.Pedidos;
using DWShop.Client.Infrastructure.Extensions;
using DWShop.Client.Infrastructure.Routes.Pedidos;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Pedidos.Get
{
    public class GetPedidosManager : IGetPedidosManager
    {
        private readonly HttpClient httpClient;

        public GetPedidosManager(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IResult<IEnumerable<PedidosResponse>>> GetAllPedidos()
        {
            var response = await httpClient.GetAsync(GetPedidosEndpoint.GetAllPedidos);
            var data = await response.ToResult<IEnumerable<PedidosResponse>>();
            return data;
        }
    }
}
