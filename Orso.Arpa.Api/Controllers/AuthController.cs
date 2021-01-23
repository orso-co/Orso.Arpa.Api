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

        /// <summary>
        /// Authenticates a user with username and password
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>The created jwt token</returns>
        /// <response code="200">Returns the created jwt token</response>
        /// <response code="400">If username or password are empty</response>
        /// <response code="401">If username or password are wrong or user is locked</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
        {
            return await _authService.LoginAsync(loginDto);
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <response code="200"></response>
        /// <response code="400">If dto is not valid</response>
        /// <response code="424">If email could not be sent</response>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            await _authService.RegisterAsync(registerDto);
            return Ok();
        }

        /// <summary>
        /// Sends an email with password reset token to user
        /// </summary>
        /// <param name="forgotPassswordDto"></param>
        /// <response code="200"></response>
        /// <response code="400">If dto is not valid</response>
        /// <response code="424">If email could not be sent</response>
        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassswordDto)
        {
            await _authService.ForgotPasswordAsync(forgotPassswordDto);
            return Ok();
        }

        /// <summary>
        /// Resets the user password to the new password provided
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <response code="200"></response>
        /// <response code="400">If dto is not valid</response>
        /// <response code="404">If user could not be found</response>
        [HttpPost("resetpassword")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            await _authService.ResetPasswordAsync(resetPasswordDto);
            return Ok();
        }

        /// <summary>
        /// Changes the password of the current user
        /// </summary>
        /// <param name="changePasswordDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid</response>
        [HttpPut("password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            await _authService.ChangePasswordAsync(changePasswordDto);
            return NoContent();
        }

        /// <summary>
        /// Sets the role of a user
        /// </summary>
        /// <param name="setRoleDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid</response>
        [HttpPut("role")]
        [Authorize(Policy = AuthorizationPolicies.SetRolePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SetRole([FromBody] SetRoleDto setRoleDto)
        {
            await _authService.SetRoleAsync(setRoleDto);
            return NoContent();
        }

        /// <summary>
        /// Confirms the email address of the current user
        /// </summary>
        /// <param name="confirmEmailDto"></param>
        /// <response code="200"></response>
        /// <response code="400">If dto is not valid</response>
        /// <response code="404">If user could not be found</response>
        [HttpPost("confirmemail")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
        {
            await _authService.ConfirmEmailAsync(confirmEmailDto);
            return Ok();
        }

        /// <summary>
        /// Creates a new email confirmation token and sends it by email
        /// </summary>
        /// <param name="createEmailConfirmationTokenDto"></param>
        /// <response code="200"></response>
        /// <response code="400">If dto is not valid</response>
        /// <response code="404">If user could not be found</response>
        /// <returns></returns>
        /// <remarks>This endpoint can be called if the previous token is expired</remarks>
        [HttpPost("emailconfirmationtoken")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateNewEmailConfirmationToken([FromBody] CreateEmailConfirmationTokenDto createEmailConfirmationTokenDto)
        {
            await _authService.CreateNewEmailConfirmationTokenAsync(createEmailConfirmationTokenDto);
            return Ok();
        }

        [HttpPost("refreshtoken")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TokenDto>> RefreshAccessToken()
        {
            return await _authService.RefreshAccessTokenAsync(HttpContext.Request.Cookies["refreshToken"]);
        }
    }
}
