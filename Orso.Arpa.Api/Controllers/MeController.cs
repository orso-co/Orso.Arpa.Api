using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization;
using static Orso.Arpa.Domain.Logic.Me.SendQRCode;

namespace Orso.Arpa.Api.Controllers
{
    public class MeController : BaseController
    {
        private readonly IMeService _meService;
        private readonly IMusicianProfileService _musicianProfileService;

        public MeController(IMeService meService, IMusicianProfileService musicianProfileService)
        {
            _meService = meService;
            _musicianProfileService = musicianProfileService;
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
        /// /// <response code="424">If email could not be sent</response>
        [Authorize(Roles = RoleNames.Performer)]
        [HttpGet("qrcode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [HttpPut("appointments/{id}/participation/prediction/{predictionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> SetParticipationPrediction([FromRoute] SetMyProjectAppointmentPredictionDto setParticipationPrediction)
        {
            await _meService.SetMyAppointmentParticipationPredictionAsync(setParticipationPrediction);
            return NoContent();
        }

        /// <summary>
        /// Gets all my musicianProfiles
        /// </summary>
        /// <returns>All musicianProfiles of the current user</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Roles = RoleNames.Performer)]
        [HttpGet("profiles/musician")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MyMusicianProfileDto>>> GetMyMusicianProfiles()
        {
            Guid personId = Guid.NewGuid(); //ToDo Mira - woher bekomme ich für Me die PersonId?
            return Ok(await _musicianProfileService.GetMyAsync(personId));
        }

        /// <summary>
        /// Gets my musicianProfile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Requested musicianProfile of the current user</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Roles = RoleNames.Performer)]
        [HttpGet("profiles/musician/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MyMusicianProfileDto>> GetMyMusicianProfile([FromRoute] Guid id)
        {
            return await _musicianProfileService.GetMyByIdAsync(id);
        }

        /// <summary>
        /// Adds a new musicianProfile
        /// </summary>
        /// <param name="myMusicianProfileCreateDto"></param>
        /// <returns>The created musicianProfile</returns>
        /// <response code="201">Returns the created musicianProfile</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Performer)]
        [HttpPost("profiles/musician")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MyMusicianProfileDto>> AddMusicianProfile(MyMusicianProfileCreateDto myMusicianProfileCreateDto)
        {
            MyMusicianProfileDto createdMusicianProfile = await _musicianProfileService.CreateAsync(myMusicianProfileCreateDto);
            //Todo Mira: hätte hier nameof(GetById) erwartet wie bei ProjectController.AddUrl
            return CreatedAtAction(nameof(_musicianProfileService.GetByIdAsync), "MusicianProfile", new { id = createdMusicianProfile.Id }, createdMusicianProfile);
        }

        /// <summary>
        /// Modifies the specified musician profile of the current user
        /// </summary>
        /// <param name="myMusicianProfileCreateDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Performer)]
        [HttpPut("profiles/musician/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> PutMusicianProfile([FromBody] MyMusicianProfileModifyDto myMusicianProfileCreateDto)
        {
            await _meService.ModifyMyMusicianProfileAsync(myMusicianProfileCreateDto);
            return NoContent();
        }
    }
}
