using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.Auth;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> Login([FromBody]Login.Dto loginDto)
        {
            return await _authService.LoginAsync(loginDto);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> Register([FromBody]UserRegister.Dto registerDto)
        {
            return await _authService.RegisterAsync(registerDto);
        }

        [HttpPut("password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ChangePassword([FromBody]ChangePassword.Dto changePasswordDto)
        {
            await _authService.ChangePasswordAsync(changePasswordDto);
            return NoContent();
        }

        [HttpPut("role")]
        [Authorize(Policy = AuthorizationPolicies.SetRolePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SetRole([FromBody]SetRole.Dto setRoleDto)
        {
            await _authService.SetRoleAsync(setRoleDto);
            return NoContent();
        }
    }
}
