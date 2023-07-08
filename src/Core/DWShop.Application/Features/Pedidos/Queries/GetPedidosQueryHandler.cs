using AutoMapper;
using DWShop.Application.Interfaces.Repositories;
using DWShop.Application.Responses.Catalog;
using DWShop.Application.Responses.Pedidos;
using DWShop.Shared.Wrapper;
using MediatR;
using PedidosEntity = DWShop.Domain.Entities.Pedidos;

namespace DWShop.Application.Features.Pedidos.Queries
{
    public class GetPedidosQueryHandler
        : IRequestHandler<GetPedidosQuery, IResult<IEnumerable<PedidosResponse>>>
    {
        private readonly IRepositoryAsync<PedidosEntity, int> _repository;
        private readonly IMapper _mapper;

        public GetPedidosQueryHandler(IRepositoryAsync<PedidosEntity, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IResult<IEnumerable<PedidosResponse>>> Handle(GetPedidosQuery request, CancellationToken cancellationToken)
        {
            var pedidios = await _repository.GetAllAsync();

            var pedidosResponse = _mapper.Map<List<PedidosResponse>>(pedidios);

            return await Result<List<PedidosResponse>>.SuccessAsync(pedidosResponse, "");
        }
    }
}
