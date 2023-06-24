using DWShop.Web.Infrastructure.Settings;
using MudBlazor;

namespace DWShop.Web.Client.Shared
{
    public partial class MainLayout
    {
        private MudTheme currentTheme;

        protected override async Task OnInitializedAsync()
        {
            currentTheme = await _clientPreference.GetCurrentThemeAsync();
            await base.OnInitializedAsync();
        }

        private async Task DarkMode()
        {
            bool isDarkMode = await _clientPreference.ToggleDarkModeAsync();

            currentTheme = isDarkMode ? DWTheme.DefautTheme : DWTheme.DarkTheme;
        }
    }
}
