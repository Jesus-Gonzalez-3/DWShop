using AutoMapper;
using DWShop.Application.Features.Pedidos.Commands.Create;
using DWShop.Application.Responses.Pedidos;
using DWShop.Domain.Entities;

namespace DWShop.Application.Mappings
{
    public class PedidosProfile : Profile
    {
        public PedidosProfile()
        {
            CreateMap<Pedidos, PedidosResponse>().ReverseMap();
            CreateMap<Pedidos, CreatePedidosCommand>().ReverseMap();
        }
    }
}
