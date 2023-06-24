using Blazored.LocalStorage;
using DWShop.Web.Infrastructure.Settings;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Web.Infrastructure.Services.ClientPreference
{
    public class ClientPreferenceService
    {
        private readonly ILocalStorageService localStorageService;

        public ClientPreferenceService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task<MudTheme> GetCurrentThemeAsync()
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference is not null)
                if (preference.IsDarkMode)
                    return DWTheme.DarkTheme;
            return DWTheme.DefautTheme;

        }

        public async Task<bool> ToggleDarkModeAsync()
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference is not null)
            {
                preference.IsDarkMode = !preference.IsDarkMode;
                await localStorageService.SetItemAsync<ClientPreference>("clientPreference", preference);
                return !preference.IsDarkMode;
            }
            return false;

        }

        public async Task<IPreference> GetPreference()
        {
            var preferences = await localStorageService.GetItemAsync<ClientPreference>("clientPreference")
                ?? new ClientPreference();
            return preferences;
        }
    }

    public interface IPreference
    {

    }

    public record ClientPreference : IPreference
    {
        public bool IsDarkMode { get; set; }
    }
}
