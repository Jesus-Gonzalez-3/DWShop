using AutoMapper;
using DWShop.Application.Features.Identitty.Commands.Login;
using DWShop.Application.Interfaces.Services;
using DWShop.Application.Responses.Identity;
using DWShop.Domain.Entities;
using DWShop.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DWShop.Application.Features.Identitty.Commands.Register
{
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<LoginResponse>>
    {
        private readonly IMediator mediator;
        private readonly UserManager<DWUser> userManager;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;

        public RegisterUserCommandHandler(IMediator mediator,
            UserManager<DWUser> userManager,
            IMapper mapper,
            IAccountService accountService)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.mapper = mapper;
            this.accountService = accountService;
        }
        public async Task<Result<LoginResponse>> Handle(RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = mapper.Map<DWUser>(request);
            if (await accountService.UserExists(request.UserName))
                return await Result<LoginResponse>.FailAsync("El usuario ya existe");
            //Creamos el usuario
            user.Id = Guid.NewGuid().ToString();
            var result = await userManager.CreateAsync(user, request.Password);

            //Verificamos si no se creo bien el usuario
            if (!result.Succeeded)
                return await Result<LoginResponse>.FailAsync(result.Errors.Select(x => x.Description).ToList());

            //Vamos a obtner el token si se creo bien el usuario y logearlo
            var loginCommand = new LoginCommand { Password = request.Password, UserName = request.UserName};

            return await mediator.Send(loginCommand);

        }
    }
}
