using AutoMapper;
using DWShop.Application.Features.Catalog.Queries;
using DWShop.Application.Features.Pedidos.Queries;
using DWShop.Application.Interfaces.Repositories;
using DWShop.Application.Responses.Pedidos;
using Moq;
using PedidosEntity = DWShop.Domain.Entities.Pedidos;
namespace DWShop.Test.Application.Feactures.Pedidos
{
    public  class GetPedidosQueryTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryAsync<PedidosEntity, int>> _repositoryMock;
   
        public GetPedidosQueryTest()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepositoryAsync<PedidosEntity, int>>();

        }
        [Fact]
        public async Task Handle_Should_Return_Success_Result()
        {
            //Arrange
            var query = new GetPedidosQuery();
            var pedidos = new List<PedidosEntity>()
            {
                new ()
                {
                    UserName = "Yisus",
                    Fecha = "2023-07-07",
                    Id = 1,
                    TotalPrice = 50
                },
                new ()
                {
                   UserName = "Nayeli",
                    Fecha = "2023-07-08",
                    Id = 2,
                    TotalPrice = 1500
                }
            };
            var response = new List<PedidosResponse>
            {
                new ()
                {
                   UserName = "Yisus",
                    Fecha = "2023-07-07",
                    Id = 1,
                    TotalPrice = 50
                },
                new ()
                {
                    UserName = "Nayeli",
                    Fecha = "2023-07-08",
                    Id = 2,
                    TotalPrice = 1500
                }
            };

            _mapperMock.Setup(x => x.Map<List<PedidosResponse>>(pedidos)).Returns(response);

            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(pedidos);

            var handler = new GetPedidosQueryHandler(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Succeded);
            Assert.Equal(response, result.Data);
            _mapperMock.Verify(x => x.Map<List<PedidosResponse>>(It.IsAny<List<PedidosEntity>>()), Times.Once());
            _repositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}
