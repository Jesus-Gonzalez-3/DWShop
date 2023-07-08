using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Application.Features.Catalog.Commands.Delete;
using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Application.Features.Catalog.Queries;
using DWShop.Application.Features.Pedidos.Commands.Create;
using DWShop.Application.Features.Pedidos.Commands.Delete;
using DWShop.Application.Features.Pedidos.Commands.Update;
using DWShop.Application.Features.Pedidos.Queries;
using DWShop.Application.Responses.Catalog;
using DWShop.Application.Responses.Pedidos;
using DWShop.Shared.Wrapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IResult = DWShop.Shared.Wrapper.IResult;

namespace DWShop.Service.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PedidosController: BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IResult<IEnumerable<PedidosResponse>>>> GetAll([FromQuery] GetPedidosQuery query)
            => Ok(await mediator.Send(query));

        [HttpPost]
        public async Task<ActionResult<IResult<int>>> CreatePedido([FromBody] CreatePedidosCommand command)
            => Ok(await mediator.Send(command));

        [HttpPut]
        public async Task<ActionResult<IResult>> UpdateProduct([FromBody] UpdatePedidoCommand command)
           => Ok(await mediator.Send(command));

        [HttpDelete]
        public async Task<ActionResult<IResult>> DeleteProduct([FromQuery] DeletePedidosCommand command
            ) => Ok(await mediator.Send(command));
    }
}
