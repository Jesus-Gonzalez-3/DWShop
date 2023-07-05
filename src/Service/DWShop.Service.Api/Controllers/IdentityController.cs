using DWShop.Application.Features.Identitty.Commands.Login;
using DWShop.Application.Features.Identitty.Commands.Register;
using DWShop.Application.Responses.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    public class IdentityController: BaseApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCommand command)
            => Ok(await mediator.Send(command));

        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterUserCommand command)
            => Ok(await mediator.Send(command));

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Check")]
        public async Task<ActionResult> Check() => Ok();
    }
}
