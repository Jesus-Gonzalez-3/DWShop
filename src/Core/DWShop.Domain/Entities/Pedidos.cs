using DWShop.Domain.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWShop.Domain.Entities
{
    [Table(nameof(Pedidos), Schema = "dbo")]
    public class Pedidos: AuditableEntity<int>
    {
        public string UserName { get; set; } = null!;
        public string Fecha { get; set; } = null!;
        public decimal TotalPrice { get; set; } = decimal.Zero;
    }
}
