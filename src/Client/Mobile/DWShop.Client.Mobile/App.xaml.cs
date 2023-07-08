using DWShop.Client.Mobile.Services;
using DWShop.Client.Mobile.Views;
using Microsoft.Maui.Controls;

namespace DWShop.Client.Mobile
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(LoginView loginView)
        {
            InitializeComponent();
            MainPage = loginView;
        }
    }
}