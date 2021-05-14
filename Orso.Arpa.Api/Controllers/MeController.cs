using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMeService _meService;

        public MeController(IMeService meService)
        {
            _meService = meService;
        }

        /// <summary>
        /// Gets the user profile of the current user
        /// </summary>
        /// <returns>The user profile of the current user</returns>
        /// <response code="200"></response>
        [HttpGet("profiles/user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MyProfileDto>> GetMyProfile()
        {
            return await _meService.GetMyProfileAsync();
        }

        /// <summary>
        /// Sends the qr code by mail and returns it as png
        /// </summary>
        /// <response code="200"></response>
        /// /// <response code="424">If email could not be sent</response>
        [HttpGet("qrcode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = RoleNames.Performer)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult> SendQrCode()
        {
            QrCodeFile qrCode = await _meService.SendMyQrCodeAsync();
            return File(qrCode.Content, QrCodeFile.ContentType, qrCode.FileName);
        }

        /// <summary>
        /// Gets the appointments of the current user
        /// </summary>
        /// <returns>The user profile of the current user</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("appointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MyAppointmentListDto>> GetMyAppointments(
            [FromQuery] int? limit,
            [FromQuery] int? offset)
        {
            return Ok(await _meService.GetMyAppointmentsAsync(limit, offset));
        }

        /// <summary>
        /// Modifies the user profile of the current user
        /// </summary>
        /// <param name="userProfileModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [HttpPut("profiles/user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> PutProfile([FromBody] MyProfileModifyDto userProfileModifyDto)
        {
            await _meService.ModifyMyProfileAsync(userProfileModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Sets the prediction of an appointment participation for the current user
        /// </summary>
        /// <param name="setParticipationPrediction"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpPut("appointments/{id}/participation/prediction/{predictionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> SetParticipationPrediction([FromRoute] SetMyProjectAppointmentPredictionDto setParticipationPrediction)
        {
            await _meService.SetMyAppointmentParticipationPredictionAsync(setParticipationPrediction);
            return NoContent();
        }
    }
}
