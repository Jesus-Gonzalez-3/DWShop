using Blazored.LocalStorage;
using DWShop.Shared.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace DWShop.Web.Infrastructure.Authentication
{
    public class DWStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public DWStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task StateChangedAsync()
        {
            var authState = Task.FromResult(await GetAuthenticationStateAsync());
            NotifyAuthenticationStateChanged(authState);
        }

        public void MaskAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var autState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(autState);
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken);
            if (string.IsNullOrWhiteSpace(savedToken))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(GetClaimsFromJwt(savedToken), "jwt")));

            var user = state.User;
            var exp = user.FindFirst(x => x.Type.Equals("exp"))?.Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));

            var diff = expTime - DateTimeOffset.UtcNow;
            if (diff.TotalMinutes >= 1)
                return state;

            await localStorage.RemoveItemAsync(StorageConstants.Local.AuthToken);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        }

        private IEnumerable<Claim> GetClaimsFromJwt(string jwt)
        {
            byte[] ParseBase64(string payload)
            {
                payload = payload.Trim().Replace('-', '+').Replace('_', '/');
                var base64 = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
                return Convert.FromBase64String(base64);
            }
            var claims = new List<Claim>();
            var payload = jwt.Split(".")[1];
            var jsonBytes = ParseBase64(payload);
            var keyValuePais = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);


            if (keyValuePais is not null)
            {
                keyValuePais.TryGetValue(ClaimTypes.Role, out var roles);
                if (roles is not null)
                {
                    if (roles.ToString().Trim().StartsWith("["))
                    {
                        var parseRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);
                        claims.AddRange(parseRoles!.Select(x => new Claim(ClaimTypes.Role, x)));
                    }
                    else
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
                    keyValuePais.Remove(ClaimTypes.Role);
                }
                claims.AddRange(keyValuePais.Select(x => new Claim(x.Key, x.Value.ToString()!)));
            }
            return claims;

        }
    }
}
