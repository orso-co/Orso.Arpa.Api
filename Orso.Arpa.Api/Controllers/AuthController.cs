using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Auth;

namespace Orso.Arpa.Api.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> Register(Register.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("password")]
        public async Task<ActionResult<Unit>> ChangePassword(ChangePassword.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
