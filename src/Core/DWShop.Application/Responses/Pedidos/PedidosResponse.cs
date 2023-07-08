using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Application.Responses.Pedidos
{
    public class PedidosResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Fecha { get; set; } = null!;
        public decimal TotalPrice { get; set; } = 0;
    }
}
