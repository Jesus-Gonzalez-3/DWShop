﻿using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Infrastructure.Managers.Products.Get;
using DWShop.Client.Mobile.Messages;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.Services;
using DWShop.Client.Mobile.ViewModels.Base;
using DWShop.Client.Mobile.Views;
using DWShop.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DWShop.Client.Mobile.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        private ProductModel productModel;
        private readonly IGetProductsManager productsManager;
        private readonly UtilityService utilityService;
        private readonly ProductView productView;
        private ObservableCollection<ProductModel> products = new();
        public ObservableCollection<ProductModel> Products { get => products; set => SetProperty(ref products, value); }

        public ICommand DetailCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public ProductListViewModel(ProductModel productModel, IGetProductsManager productsManager, UtilityService utilityService,
            ProductView productView)
        {
            this.productModel = productModel;
            this.productsManager = productsManager;
            this.utilityService = utilityService;
            this.productView = productView;
            if (!WeakReferenceMessenger.Default.IsRegistered<string>(""))
                WeakReferenceMessenger.Default.Register<string>("", async (o, s) =>
                {
                    await LoadProducts();
                });
            DetailCommand = new Command<ProductModel>(ShowDetail);
            RefreshCommand = new Command<ProductListView>(async x=> { await LoadProducts(); });
        }

        public void ShowDetail(ProductModel productModel)
        {
            Navigation.PushAsync(productView);
            WeakReferenceMessenger.Default.Send(new ProductDetailMessage { Data = productModel});
        }

        public async Task LoadProducts()
        {
            if (!await utilityService.IsAuthenticated())
                return;

            IsBussy = true;
            var response = await productsManager.GetAllProducts();
            if (response.Succeded)
            {
                //TODO : map
                Products = new ObservableCollection<ProductModel>(response.Data.Select(x => new ProductModel
                {
                    Id = x.Id,
                    PhotoURL = x.PhotoURL,
                    Price = x.Price,
                    ProductName = x.Name
                }).ToList());

                IsBussy = false;
            }
        }

    }
}
