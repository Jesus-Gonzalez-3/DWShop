using CommunityToolkit.Mvvm.Messaging;
using DWShop.Application.Features.Identitty.Commands.Login;
using DWShop.Client.Infrastructure.Managers.Authentication;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using DWShop.Client.Mobile.Views;
using DWShop.Shared.Constants;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace DWShop.Client.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationManager authenticationManager;
        private LoginModel loginModel;
        public LoginModel LoginModel
        {
            get => loginModel;
            set=> SetProperty(ref loginModel, value);
        }
        public ICommand LoginCommand { get; private set; }

        public LoginViewModel(IAuthenticationManager authenticationManager, LoginModel loginModel, HttpClient httpClient, ProductListView productListView)
        {
            this.authenticationManager = authenticationManager;
            this.loginModel = loginModel;
            LoginCommand = new Command(async () =>
            {
                var result = await authenticationManager.Login(
                    new LoginCommand
                    {
                        UserName = loginModel.UserName,
                        Password = loginModel.Password,
                    }
                    );

                if (result.Succeded)
                {
                    await SecureStorage.Default.SetAsync(StorageConstants.Local.AuthTokenMovile, result.Data.Token);
                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(StorageConstants.Local.Scheme, result.Data.Token);


                    Microsoft.Maui.Controls.Application.Current.MainPage = new NavigationPage(productListView);
                    //Cargar Productos
                    WeakReferenceMessenger.Default.Send<string>(StorageConstants.Local.Cargar);
                    return;
                }

                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Error al Iniciar",
                    result.Messages.First(), "OK");
            });
        }

        
    }
}
