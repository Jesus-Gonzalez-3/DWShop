﻿using AutoMapper;
using DWShop.Application.Interfaces.Services;
using DWShop.Application.Responses.Identity;
using DWShop.Domain.Entities;
using DWShop.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Application.Features.Identitty.Commands.Login
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IAccountService accountService;
        private readonly UserManager<DWUser> userManager;
        private readonly SignInManager<DWUser> signInManager;
        private readonly IMapper mapper;

        public LoginCommandHandler(IAccountService accountService, UserManager<DWUser> userManager,
            SignInManager<DWUser> signInManager, IMapper mapper)
        {
            this.accountService = accountService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, 
            CancellationToken cancellationToken)
        {
            //Inicia la validacion del usuario
            if(!await accountService.UserExists(request.UserName.ToLower()))
                return await Result<LoginResponse>.FailAsync("Usuario no valido");

            var user = await userManager.Users.SingleAsync(x => x.UserName!.ToLower() == request.UserName.ToLower());

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if(!result.Succeeded)
                return await Result<LoginResponse>.FailAsync("Usuario no valido");
            //Finaliza la validacion 

            //generamos el token
            var token = await accountService.GetToken(user);

            var loginResult = mapper.Map<LoginResponse>(user);

            loginResult.Token = token;

            return await Result<LoginResponse>.SuccessAsync(loginResult);
        }
    }
}
