using DWShop.Application.Features.Catalog.Queries.GetById;
using DWShop.Application.Interfaces.Repositories;
using Moq;
using CatalogEntity = DWShop.Domain.Entities.Catalog;

namespace DWShop.Test.Application.Feactures.Catalog
{
    public class GetByIdCatalogQueryTest
    {
        private readonly Mock<IRepositoryAsync<CatalogEntity, int>> _repositoryMock;
        public GetByIdCatalogQueryTest()
        {
            _repositoryMock = new Mock<IRepositoryAsync<CatalogEntity, int>>();
        }

        [Fact]
        public async Task Handle_Should_Return_Success_Result()
        {
            //Arrange
            var query = new GetByIdCatalogQuery();
            var catalogs = new List<CatalogEntity>()
            {
                new ()
                {
                    Name = "Teclado",
                    Category = "Computacion",
                    Id = 1,
                    PhotoURL = "photo",
                    Price = 12.5M,
                    Summary = "Bonito teclado mecanico iluminador",
                    Description = "Teclado marca corsair mecanico teclas azules"
                },
                new ()
                {
                    Name = "Mouse",
                    Category = "Computacion",
                    Id = 2,
                    PhotoURL = "photo",
                    Price = 120.5M,
                    Summary = "Bonito mouse iluminador",
                    Description = "Mouse"
                }
            };
            var response = new CatalogEntity
            {
                Name = "Teclado",
                Category = "Computacion",
                Id = 2,
                PhotoURL = "photo",
                Price = 12.5M,
                Summary = "Bonito teclado mecanico iluminador",
                Description = "Teclado marca corsair mecanico teclas azules"
            };


            _repositoryMock.Setup(x => x.GetByIdAsync(response.Id)).ReturnsAsync(response);

            var handler = new GetByIdCatalogQueryHandler(_repositoryMock.Object);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(x => x.GetByIdAsync(response.Id), Times.AtMostOnce);
        }

    }
}
