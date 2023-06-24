using DWShop.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Application.Features.Catalog.Commands.Delete
{
    public class DeleteCatalogCommand: IRequest<IResult>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

    }
}
