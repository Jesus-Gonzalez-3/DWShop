using DWShop.Client.Infrastructure.Managers.Products.Get;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Client.Mobile.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        private ProductModel productModel;
        private readonly IGetProductsManager productsManager;

        private ObservableCollection<ProductModel> products = new();
        public ObservableCollection<ProductModel> Products { get => products; set => SetProperty(ref products, value); }

        public ProductListViewModel(ProductModel productModel, IGetProductsManager productsManager)
        {
            this.productModel = productModel;
            this.productsManager = productsManager;
            LoadProducts();
        }

        public async void LoadProducts()
        {
            //return;
            IsBussy = true;
            var response = await productsManager.GetAllProducts();
            if (response.Succeded)
            {

                Products = new ObservableCollection<ProductModel>(response.Data.Select(x => new ProductModel
                {
                    Id = x.Id,
                    PhothoUrl = x.PhotoURL,
                    Price = x.Price,
                    ProductName = x.Name
                }).ToList());

                IsBussy = false;
            }
        }

    }
}
