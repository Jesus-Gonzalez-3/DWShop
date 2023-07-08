using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Mobile.Context;
using DWShop.Client.Mobile.Messages;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using DWShop.Shared.Constants;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System.Windows.Input;

namespace DWShop.Client.Mobile.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        ProductModel product;
        private readonly DataContext dataContext;
        public bool flash = false;
        public ICommand AddToBasketCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand FlashCommand { get; set; }

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
            TakePhotoCommand = new Command(async x => await TakePhoto());
            FlashCommand = new Command(async x => await Flash());
        }

        async Task AddToBasket()
        {
            var added = await dataContext.AddToBasket(Product);
            await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(StorageConstants.Local.Add,
                added ? StorageConstants.Local.OK : StorageConstants.Local.ERROR,
                "OK");
            await Navigation.PopAsync();
            WeakReferenceMessenger.Default.Send(new RefreshBasketMessage());
        }

        async Task TakePhoto()
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
             Product.PhotoURL = photo.FullPath;

            /*Acceder a los binarios de la foto*/
            var imgSource = ImageSource.FromStream(async x => await photo.OpenReadAsync());
        }

        async Task Flash()
        {
            if (!flash)
                await Flashlight.Default.TurnOnAsync();
            else
                await Flashlight.Default.TurnOffAsync();

            flash = !flash;
        }

    }
}
