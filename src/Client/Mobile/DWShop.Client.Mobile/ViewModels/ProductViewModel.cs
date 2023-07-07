using Android.App;
using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Mobile.Context;
using DWShop.Client.Mobile.Messages;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using DWShop.Shared.Constants;
using System.Windows.Input;

namespace DWShop.Client.Mobile.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        ProductModel product;
        private readonly DataContext dataContext;

        public ICommand AddToBasketCommand { get; set; }

        public ProductModel Product
        {
            get => product;
            set => SetProperty(ref product, value);
        }

        public ProductViewModel(DataContext dataContext)
        {
            if (!WeakReferenceMessenger.Default.IsRegistered<ProductDetailMessage>(""))
                WeakReferenceMessenger.Default.Register<ProductDetailMessage>("", (o, s) =>
                {
                    Product = s.Data;
                });
            this.dataContext = dataContext;
            AddToBasketCommand = new Command(async x => await AddToBasket());
        }

        async Task AddToBasket()
        {
            var added = await dataContext.AddToBasket(Product);
            await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(StorageConstants.Local.Add,
                added ? StorageConstants.Local.OK  : StorageConstants.Local.ERROR ,
                "OK");
            await Navigation.PopAsync();
            WeakReferenceMessenger.Default.Send(new RefreshBasketMessage());
        }

    }
}
