using DWShop.Application.Responses.Catalog;
using DWShop.Client.Infrastructure.Extensions;
using DWShop.Client.Infrastructure.Routes.Products.Get;
using DWShop.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Client.Infrastructure.Managers.Products.Get
{
    public class GetProductsManager : IGetProductsManager
    {
        private readonly HttpClient httpClient;

        public GetProductsManager(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IResult<IEnumerable<ProductResponse>>> GetAllProducts()
        {
            var response = await httpClient.GetAsync(GetProductsEndpoints.GetAllProducts);
            var data = await response.ToResult<IEnumerable<ProductResponse>>();
            return data;
        }
    }
}
