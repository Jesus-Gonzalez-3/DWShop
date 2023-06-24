using DWShop.Application.Features.Identitty.Commands.Login;
using DWShop.Application.Responses.Identity;
using DWShop.Client.Infrastructure.Extensions;
using DWShop.Client.Infrastructure.Routes.Authentication;
using DWShop.Shared.Wrapper;
using System.Net.Http.Json;

namespace DWShop.Client.Infrastructure.Managers.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly HttpClient _httpClient;

        public AuthenticationManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<LoginResponse>> Login(LoginCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync(AuthenticationEndpoints.Login, command);
            var result = await response.ToResult<LoginResponse>();
            return result;
        }
    }
}
