﻿using DWShop.Client.Infrastructure.Managers.Authentication;
using DWShop.Client.Infrastructure.Managers.Products.Get;
using DWShop.Client.Infrastructure.Routes;
using DWShop.Client.Mobile.Context;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.Services;
using DWShop.Client.Mobile.ViewModels;
using DWShop.Client.Mobile.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;

namespace DWShop.Client.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterManagers()
                .RegisterViews()
                .RegisterModels()
                .RegisterViewModels()
                .RegisterServices();


            builder.Services.AddScoped(sp => new HttpClient()
            {
                BaseAddress = new Uri(BaseConfiguration.BaseAddress)
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterManagers(this MauiAppBuilder mauiAppBuilder) {
            mauiAppBuilder.Services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            mauiAppBuilder.Services.AddTransient<IGetProductsManager, GetProductsManager>();
            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddTransient<MainPage>();
            appBuilder.Services.AddTransient<LoginView>();
            appBuilder.Services.AddTransient<ProductListView>();
            appBuilder.Services.AddTransient<ProductView>();
            appBuilder.Services.AddTransient<MainTabbedPage>();
            appBuilder.Services.AddTransient<BasketView>();
            return appBuilder;
        }

        private static MauiAppBuilder RegisterViewModels( this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<LoginViewModel>();
            mauiAppBuilder.Services.AddTransient<ProductViewModel>();
            mauiAppBuilder.Services.AddTransient<ProductListViewModel>();
            mauiAppBuilder.Services.AddTransient<BasketViewModel>();
            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterModels ( this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddTransient<LoginModel>();
            appBuilder.Services.AddTransient<ProductModel>();
            return appBuilder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddTransient<UtilityService>();
            appBuilder.Services.AddTransient<DataContext>();
            return appBuilder;
        }

    }
}