using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Interfaces;
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
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
        {
            return await _authService.LoginAsync(loginDto);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> Register([FromBody] UserRegisterDto registerDto)
        {
            return await _authService.RegisterAsync(registerDto);
        }

        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassswordDto)
        {
            await _authService.ForgotPasswordAsync(forgotPassswordDto);
            return NoContent();
        }

        [HttpPost("resetpassword")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            await _authService.ResetPasswordAsync(resetPasswordDto);
            return NoContent();
        }

        [HttpPut("password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            await _authService.ChangePasswordAsync(changePasswordDto);
            return NoContent();
        }

        [HttpPut("role")]
        [Authorize(Policy = AuthorizationPolicies.SetRolePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SetRole([FromBody] SetRoleDto setRoleDto)
        {
            await _authService.SetRoleAsync(setRoleDto);
            return NoContent();
        }
    }
}
