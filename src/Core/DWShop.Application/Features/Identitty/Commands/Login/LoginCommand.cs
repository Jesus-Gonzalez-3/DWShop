using DWShop.Application.Responses.Identity;
using DWShop.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Application.Features.Identitty.Commands.Login
{
    public class LoginCommand : IRequest<Result<LoginResponse>>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
