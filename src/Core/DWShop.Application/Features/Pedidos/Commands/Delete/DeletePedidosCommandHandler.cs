using DWShop.Application.Interfaces.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;
using PedidosEntity = DWShop.Domain.Entities.Pedidos;

namespace DWShop.Application.Features.Pedidos.Commands.Delete
{
    public class DeletePedidosCommandHandler : IRequestHandler<DeletePedidosCommand, IResult>
    {
        private readonly IRepositoryAsync<PedidosEntity, int> repositoryAsync;

        public DeletePedidosCommandHandler(IRepositoryAsync<PedidosEntity, int> repositoryAsync)
        {
            this.repositoryAsync = repositoryAsync;
        }
        public async Task<IResult> Handle(DeletePedidosCommand request, CancellationToken cancellationToken)
        {
            var pedidos = await repositoryAsync.GetByIdAsync(request.Id);
            if (pedidos is null)
                return await Result.FailAsync("Pedido no encontrado");
            await repositoryAsync.DeleteAsync(pedidos);
            await repositoryAsync.SaveChangesAsync();
            return await Result.SuccessAsync();
        }
    }
}
