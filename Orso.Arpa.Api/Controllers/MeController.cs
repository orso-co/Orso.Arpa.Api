using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization;
using static Orso.Arpa.Domain.Logic.Me.SendQRCode;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/users/me")]
    public class MeController : BaseController
    {
        private readonly IUserService _userService;

        public MeController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets the user profile of the current user
        /// </summary>
        /// <returns>The user profile of the current user</returns>
        /// <response code="200"></response>
        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MyProfileDto>> GetMyProfile()
        {
            return await _userService.GetProfileOfCurrentUserAsync();
        }

        /// <summary>
        /// Sends the qr code by mail and returns it as png
        /// </summary>
        /// <response code="200"></response>
        /// /// <response code="424">If email could not be sent</response>
        [HttpGet("qrcode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = RoleNames.Performer)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult> SendQrCode()
        {
            QrCodeFile qrCode = await _userService.SendQrCodeAsync();
            return File(qrCode.Content, QrCodeFile.ContentType, qrCode.FileName);
        }

        /// <summary>
        /// Gets the appointments of the current user
        /// </summary>
        /// <returns>The user profile of the current user</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastPerformerPolicy)]
        [HttpGet("appointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MyAppointmentListDto>> GetMyAppointments(
            [FromQuery] int? limit,
            [FromQuery] int? offset)
        {
            return Ok(await _userService.GetAppointmentsOfCurrentUserAsync(limit, offset));
        }

        /// <summary>
        /// Modifies the user profile of the current user
        /// </summary>
        /// <param name="userProfileModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid</response>
        [HttpPut("profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutProfile([FromBody] MyProfileModifyDto userProfileModifyDto)
        {
            await _userService.ModifyProfileOfCurrentUserAsync(userProfileModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Sets the prediction of an appointment participation for the current user
        /// </summary>
        /// <param name="setParticipationPrediction"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or if appointment or participation or prediction value could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastPerformerPolicy)]
        [HttpPut("appointments/{id}/participation/prediction/{predictionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SetParticipationPrediction([FromRoute] SetMyProjectAppointmentPredictionDto setParticipationPrediction)
        {
            await _userService.SetAppointmentParticipationPredictionAsync(setParticipationPrediction);
            return NoContent();
        }
    }
}
