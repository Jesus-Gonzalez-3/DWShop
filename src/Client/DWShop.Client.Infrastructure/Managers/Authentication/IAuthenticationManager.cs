﻿using DWShop.Application.Features.Identitty.Commands.Login;
using DWShop.Application.Responses.Identity;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Authentication
{
    public interface IAuthenticationManager :IManager
    {
        Task<IResult<LoginResponse>> Login(LoginCommand command);
    }
}