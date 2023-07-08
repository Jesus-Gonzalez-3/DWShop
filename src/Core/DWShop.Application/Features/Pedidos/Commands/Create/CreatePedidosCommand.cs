using DWShop.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Application.Features.Pedidos.Commands.Create
{
    public class CreatePedidosCommand: IRequest<IResult<int>>
    {
        public string UserName { get; set; }
        public string Fecha { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
