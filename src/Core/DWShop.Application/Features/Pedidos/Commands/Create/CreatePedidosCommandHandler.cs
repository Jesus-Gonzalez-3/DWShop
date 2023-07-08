using AutoMapper;
using DWShop.Application.Interfaces.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;
using PedidosEntity = DWShop.Domain.Entities.Pedidos;

namespace DWShop.Application.Features.Pedidos.Commands.Create
{
    public class CreatePedidosCommandHandler
        : IRequestHandler<CreatePedidosCommand, IResult<int>>
    {
        private readonly IRepositoryAsync<PedidosEntity, int> repositoryAsync;
        private readonly IMapper mapper;

        public CreatePedidosCommandHandler(
            IRepositoryAsync<PedidosEntity, int>repositoryAsync, IMapper mapper)
        {
            this.repositoryAsync = repositoryAsync;
            this.mapper = mapper;
        }
        public async Task<IResult<int>> Handle(CreatePedidosCommand request, CancellationToken cancellationToken)
        {
            var pedido = mapper.Map<PedidosEntity>(request);
            await repositoryAsync.AddAsync(pedido);
            await repositoryAsync.SaveChangesAsync();

            return await Result<int>.SuccessAsync(pedido.Id, "");
        }
    }
}
