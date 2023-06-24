using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Application.Features.Catalog.Commands.Delete;
using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Client.Infrastructure.Extensions;
using DWShop.Client.Infrastructure.Routes.Products.AddEditDelete;
using DWShop.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Client.Infrastructure.Managers.Products.Edit
{
    
    public class AddEditDeleteProductManager : IAddEditDeleteProductManager
    {
        private const string Uid = "?Id=";
        private readonly HttpClient _httpClient;

        public AddEditDeleteProductManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult> EditProduct(UpdateCatalogCommand command)
        {
            var response = await _httpClient.PutAsJsonAsync(AddEditDeleteProductsEndpoints.Edit, command);
            return await response.ToResult();
        }

        public async Task<IResult> DeleteProduct(DeleteCatalogCommand command)
        {
            var response = await _httpClient.DeleteAsync(AddEditDeleteProductsEndpoints.Edit + Uid + command.Id);
            return await response.ToResult();
        }

        public async Task<IResult> AddProduct(CreateCatalogCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync(AddEditDeleteProductsEndpoints.Edit, command);
            return await response.ToResult();
        }
    }
}
