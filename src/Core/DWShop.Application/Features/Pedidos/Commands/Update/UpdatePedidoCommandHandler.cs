using DWShop.Application.Interfaces.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;
using PedidosEntity = DWShop.Domain.Entities.Pedidos;

namespace DWShop.Application.Features.Pedidos.Commands.Update
{
    public class UpdatePedidoCommandHandler : IRequestHandler<UpdatePedidoCommand, IResult>
    {
        private readonly IRepositoryAsync<PedidosEntity, int> repositoryAsync;

        public UpdatePedidoCommandHandler(IRepositoryAsync<PedidosEntity, int> repositoryAsync)
        {
            this.repositoryAsync = repositoryAsync;
        }
        public async Task<IResult> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
        {
            PedidosEntity? entity = await repositoryAsync.GetByIdAsync(request.Id);
            if (entity is null)
                return await Result.FailAsync("Pedido no encontrado");

            entity.UserName = request.UserName;
            entity.Fecha = request.Fecha;
            entity.TotalPrice = request.TotalPrice;

            await repositoryAsync.UpdateAsync(entity);
            await repositoryAsync.SaveChangesAsync();
            return await Result.SuccessAsync();
        }
    }
}
