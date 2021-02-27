using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private string RefreshToken => HttpContext.Request.Cookies["refreshToken"];
        private string RemoteIpAddress => HttpContext.Connection.RemoteIpAddress.ToString();

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user with username and password.
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>The created jwt access token. Sets refreshtoken Cookie</returns>
        /// <response code="200"></response>
        /// <response code="400">If username or password are empty or if email address is not yet confirmed</response>
        /// <response code="401">If username or password are incorrect or user is locked</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
        {
            return await _authService.LoginAsync(loginDto, RemoteIpAddress);
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid</response>
        /// <response code="424">If email could not be sent</response>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            await _authService.RegisterAsync(registerDto);
            return NoContent();
        }

        /// <summary>
        /// Sends an email with password reset token to user
        /// </summary>
        /// <param name="forgotPassswordDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid</response>
        /// <response code="424">If email could not be sent</response>
        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassswordDto)
        {
            await _authService.ForgotPasswordAsync(forgotPassswordDto);
            return NoContent();
        }

        /// <summary>
        /// Resets the user password to the new password provided
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or token is expired or invalid or if user could not be found</response>
        [HttpPost("resetpassword")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            await _authService.ResetPasswordAsync(resetPasswordDto);
            return NoContent();
        }

        /// <summary>
        /// Changes the password of the current user
        /// </summary>
        /// <param name="changePasswordDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid</response>
        [HttpPut("password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SetRole([FromBody] SetRoleDto setRoleDto)
        {
            await _authService.SetRoleAsync(setRoleDto);
            return NoContent();
        }

        /// <summary>
        /// Confirms the email address of the current user
        /// </summary>
        /// <param name="confirmEmailDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or token is expired or invalid or if user could not be found</response>
        [HttpPost("confirmemail")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
        {
            await _authService.ConfirmEmailAsync(confirmEmailDto);
            return NoContent();
        }

        /// <summary>
        /// Creates a new email confirmation token and sends it by email
        /// </summary>
        /// <param name="createEmailConfirmationTokenDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or if user could not be found</response>
        /// <remarks>This endpoint can be called if the previous token is expired</remarks>
        [HttpPost("emailconfirmationtoken")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateNewEmailConfirmationToken(
            [FromBody] CreateEmailConfirmationTokenDto createEmailConfirmationTokenDto)
        {
            await _authService.CreateNewEmailConfirmationTokenAsync(createEmailConfirmationTokenDto);
            return NoContent();
        }

        /// <summary>
        /// Creates a new access and refresh token based on the existing refresh token in the refreshtoken Cookie
        /// </summary>
        /// <response code="200"></response>
        /// <response code="400">If request does not contain a refresh token cookie or no user could be found with supplied refresh token</response>
        /// <response code="401">If refresh token is not valid</response>
        /// <response code="403">If the refresh token was accessed with a different IP than it was created</response>
        /// <returns>A new access token. Sets new refresh token cookie</returns>
        [HttpPost("refreshtoken")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TokenDto>> RefreshAccessToken()
        {
            return await _authService.RefreshAccessTokenAsync(RefreshToken, RemoteIpAddress);
        }

        /// <summary>
        /// Revokes current refresh token
        /// </summary>
        /// <response code="204"></response>
        /// <response code="400">If request does not contain a refresh token cookie
        /// or no user could be found with supplied refresh token</response>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Logout()
        {
            await _authService.RevokeRefreshTokenAsync(RefreshToken, RemoteIpAddress);
            return NoContent();
        }
    }
}
