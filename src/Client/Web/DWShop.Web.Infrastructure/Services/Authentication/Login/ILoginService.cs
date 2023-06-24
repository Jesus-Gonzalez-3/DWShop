using DWShop.Application.Features.Identitty.Commands.Login;
using DWShop.Shared.Wrapper;

namespace DWShop.Web.Infrastructure.Services.Authentication.Login
{
    public interface ILoginService
    {
        Task<IResult> Login(LoginCommand model);
    }
}