using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.MeApplication.Interfaces;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Infrastructure.Authorization;
using static Orso.Arpa.Domain.UserDomain.Commands.SendMyQRCode;

namespace Orso.Arpa.Api.Controllers
{
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
        public async Task<ActionResult<MyUserProfileDto>> GetMyUserProfile()
        {
            return await _meService.GetMyUserProfileAsync();
        }

        /// <summary>
        /// Sends the qr code by mail and returns it as png
        /// </summary>
        /// <response code="200"></response>
        /// <response code="424">If email could not be sent</response>
        [Authorize(Roles = RoleNames.Performer)]
        [HttpGet("qrcode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult> SendQrCode([FromQuery] bool sendEmail = false)
        {
            QrCodeFile qrCode = await _meService.GetMyQrCodeAsync(sendEmail);
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
            [FromQuery] int? offset,
            [FromQuery] bool passed = false)
        {
            return Ok(await _meService.GetMyAppointmentsAsync(limit, offset, passed));
        }

        /// <summary>
        /// Queries a list of appointments dependent on the given date and date range
        /// </summary>
        /// <param name="date"></param>
        /// <param name="range"></param>
        /// <returns>A list of appointments</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("appointments/range")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Task<IList<MyAppointmentDto>>>> Get([FromQuery] DateTime? date, [FromQuery] DateRange range)
        {
            return Ok(await _meService.GetAppointmentRangeAsync(date, range));
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
        public async Task<ActionResult> PutUserProfile([FromBody] MyUserProfileModifyDto userProfileModifyDto)
        {
            await _meService.ModifyMyUserProfileAsync(userProfileModifyDto);
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
        [HttpPut("appointments/{id}/participation/prediction")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> SetAppointmentParticipationPrediction(SetMyAppointmentParticipationPredictionDto setParticipationPrediction)
        {
            await _meService.SetMyAppointmentParticipationPredictionAsync(setParticipationPrediction);
            return NoContent();
        }
    }
}
