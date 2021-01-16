using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Infrastructure.Authorization;
using Orso.Arpa.Mail;

namespace Orso.Arpa.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IEmailSender _emailSender;

        public AuthController(IAuthService authService, IEmailSender emailSender)
        {
            _authService = authService;
            _emailSender = emailSender;
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
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPassswordDto forgotPassswordDto)
        {
            await _authService.ForgotPasswordAsync(forgotPassswordDto);
            return NoContent();
        }

        [HttpPost("testemail")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Email()
        {
            var message = new EmailMessage(new string[] { "mira@gutfrau.de" }, "Test mail with Attachments", "<b>This is the content from our mail with attachments.</b>", true);
            await _emailSender.SendEmailAsync(message);
            return true;
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
