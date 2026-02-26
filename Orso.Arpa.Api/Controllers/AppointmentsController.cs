using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AppointmentApplication.Interfaces;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Notifications;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers
{
    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ITokenAccessor _tokenAccessor;
        private readonly IMediator _mediator;

        public AppointmentsController(IAppointmentService appointmentService, ITokenAccessor tokenAccessor, IMediator mediator)
        {
            _appointmentService = appointmentService;
            _tokenAccessor = tokenAccessor;
            _mediator = mediator;
        }

        /// <summary>
        /// Queries a list of appointments dependent on the given date and date range
        /// </summary>
        /// <param name="date"></param>
        /// <param name="range"></param>
        /// <returns>A list of appointments</returns>
        /// <response code="200"></response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppointmentListDto>>> Get([FromQuery] DateTime? date, [FromQuery] DateRange range)
        {
            return Ok(await _appointmentService.GetAsync(date, range));
        }

        /// <summary>
        /// Queries all appointments for the given date range (for all authenticated users, without internal details)
        /// </summary>
        /// <param name="date"></param>
        /// <param name="range"></param>
        /// <returns>A list of appointments without internal details</returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppointmentListDto>>> GetAll([FromQuery] DateTime? date, [FromQuery] DateRange range)
        {
            return Ok(await _appointmentService.GetAllAsync(date, range));
        }

        /// <summary>
        /// Queries recently modified appointments (for all authenticated users, without internal details)
        /// </summary>
        /// <param name="days">Number of days to look back (default: 14)</param>
        /// <returns>A list of recently modified appointments</returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet("recently-modified")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppointmentRecentlyModifiedDto>>> GetRecentlyModified([FromQuery] int days = 14)
        {
            return Ok(await _appointmentService.GetRecentlyModifiedAsync(days));
        }

        /// <summary>
        /// Gets an appointment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeParticipations">Default: true. If true, request will be very slow!</param>
        /// <returns>The queried appointment</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppointmentDto>> GetById([FromRoute] Guid id, [FromQuery] bool includeParticipations = true)
        {
            return Ok(await _appointmentService.GetByIdAsync(id, includeParticipations));
        }

        /// <summary>
        /// Creates a new appointment
        /// </summary>
        /// <param name="appointmentCreateDto"></param>
        /// <returns>The created appointment</returns>
        /// <response code="201">Returns the created appointment</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails),
            StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<AppointmentDto>> Post([FromBody] AppointmentCreateDto appointmentCreateDto)
        {
            AppointmentDto createdAppointment =
                await _appointmentService.CreateAsync(appointmentCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.Id },
                createdAppointment);
        }

        /// <summary>
        /// Coppies an existing appointment
        /// </summary>
        /// <param name="appointmentCopyDto"></param>
        /// <returns>The created appointment</returns>
        /// <response code="201">Returns the created appointment</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("copy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails),
            StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<AppointmentDto>> Copy([FromBody] AppointmentCopyDto appointmentCopyDto)
        {
            AppointmentDto createdAppointment =
                await _appointmentService.CopyAsync(appointmentCopyDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.Id },
                createdAppointment);
        }

        /// <summary>
        /// Adds a room to an existing appointment
        /// </summary>
        /// <param name="addRoomDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails),
            StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> AddRoom([FromRoute] AppointmentAddRoomDto addRoomDto)
        {
            await _appointmentService.AddRoomAsync(addRoomDto);
            return NoContent();
        }

        /// <summary>
        /// Adds a project to an existing appointment
        /// </summary>
        /// <param name="addProjectDto"></param>
        /// <param name="includeParticipations"></param>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/projects/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<AppointmentDto>> AddProject([FromRoute] AppointmentAddProjectDto addProjectDto, [FromQuery] bool includeParticipations = true)
        {
            return await _appointmentService.AddProjectAsync(addProjectDto, includeParticipations);
        }

        /// <summary>
        /// Adds a section to an existing appointment
        /// </summary>
        /// <param name="addSectionDto"></param>
        /// <param name="includeParticipations"></param>
        /// <response code="200"></response>
        /// <response code="400">If domain validation fails</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/sections/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails),
            StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<AppointmentDto>> AddSection([FromRoute] AppointmentAddSectionDto addSectionDto, [FromQuery] bool includeParticipations = true)
        {
            return await _appointmentService.AddSectionAsync(addSectionDto, includeParticipations);
        }

        /// <summary>
        /// Sends an appointment changed notification to all participants of the appointment
        /// </summary>
        /// <param name="sendAppointmentChangedNotificationDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/notification")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<AppointmentDto>> SendAppointmentChangedNotification(
            SendAppointmentChangedNotificationDto sendAppointmentChangedNotificationDto)
        {
            await _appointmentService.SendAppointmentChangedNotificationAsync(sendAppointmentChangedNotificationDto);
            return NoContent();
        }

        /// <summary>
        /// Sets the venue of an existing appointment
        /// </summary>
        /// <param name="setVenueDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/venue/set/{venueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> SetVenue([FromRoute] AppointmentSetVenueDto setVenueDto)
        {
            await _appointmentService.SetVenueAsync(setVenueDto);
            return NoContent();
        }

        /// <summary>
        /// Modifies existing appointment
        /// </summary>
        /// <param name="appointmentModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(AppointmentModifyDto appointmentModifyDto)
        {
            await _appointmentService.ModifyAsync(appointmentModifyDto);

            _ = _mediator.Publish(new AppointmentChangedNotification
            {
                AppointmentId = appointmentModifyDto.Id,
                AppointmentName = appointmentModifyDto.Body.Name,
                StartTime = appointmentModifyDto.Body.StartTime
            });

            return NoContent();
        }

        /// <summary>
        /// Removes room from existing appointment
        /// </summary>
        /// <param name="removeRoomDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails),
            StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> RemoveRoom(
            [FromRoute] AppointmentRemoveRoomDto removeRoomDto)
        {
            await _appointmentService.RemoveRoomAsync(removeRoomDto);
            return NoContent();
        }

        /// <summary>
        /// Removes section from existing appointment
        /// </summary>
        /// <param name="removeSectionDto"></param>
        /// <param name="includeParticipations"></param>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/sections/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<AppointmentDto>> RemoveSection([FromRoute] AppointmentRemoveSectionDto removeSectionDto, [FromQuery] bool includeParticipations = true)
        {
            return await _appointmentService.RemoveSectionAsync(removeSectionDto, includeParticipations);
        }

        /// <summary>
        /// Removes project from existing appointment
        /// </summary>
        /// <param name="removeProjectDto"></param>
        /// <param name="includeParticipations"></param>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/projects/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails),
            StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<AppointmentDto>> RemoveProject([FromRoute] AppointmentRemoveProjectDto removeProjectDto, [FromQuery] bool includeParticipations = true)
        {
            return await _appointmentService.RemoveProjectAsync(removeProjectDto, includeParticipations);
        }

        /// <summary>
        /// Deletes existing appointment by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _appointmentService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Sets start and end time of an existing appointment
        /// </summary>
        /// <param name="setDatesDto"></param>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/dates/set")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<AppointmentDto>> SetDates(AppointmentSetDatesDto setDatesDto)
        {
            return await _appointmentService.SetDatesAsync(setDatesDto);
        }

        /// <summary>
        /// Sets the result of an appointment participation
        /// </summary>
        /// <param name="setParticipationResult"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/participations/{personId}/result")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> SetParticipationResult(
            AppointmentParticipationSetResultDto setParticipationResult)
        {
            await _appointmentService.SetParticipationResultAsync(setParticipationResult);
            return NoContent();
        }

        /// <summary>
        /// Sets the prediction of an appointment participation
        /// </summary>
        /// <param name="setParticipationPrediction"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/participations/{personId}/prediction")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> SetParticipationPrediction(AppointmentParticipationSetPredictionDto setParticipationPrediction)
        {
            await _appointmentService.SetParticipationPredictionAsync(setParticipationPrediction);
            return NoContent();
        }

        /// <summary>
        /// Sets the staff comment of an appointment participation
        /// </summary>
        /// <param name="setCommentByStaff"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/participations/{personId}/comment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> SetParticipationCommentByStaff(AppointmentParticipationSetCommentByStaffDto setCommentByStaff)
        {
            await _appointmentService.SetParticipationCommentByStaffAsync(setCommentByStaff);
            return NoContent();
        }

        /// <summary>
        ///    Exports all appointments to ics file
        /// </summary>
        /// <response code="200"></response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("export")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportToIcs()
        {
            string serializedCalendar = await _appointmentService.ExportAppointmentsToIcsAsync();
            return File(Encoding.UTF8.GetBytes(serializedCalendar), "text/calendar",
                "appointments.ics");
        }

        /// <summary>
        /// Adds a prioritized piece to an appointment (marks a setlist piece as rehearsal focus)
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <param name="setlistPieceId">SetlistPiece ID</param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/prioritized-pieces/{setlistPieceId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> AddPrioritizedPiece([FromRoute] Guid id, [FromRoute] Guid setlistPieceId)
        {
            await _appointmentService.AddPrioritizedPieceAsync(id, setlistPieceId);
            return NoContent();
        }

        /// <summary>
        /// Removes a prioritized piece from an appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <param name="setlistPieceId">SetlistPiece ID</param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/prioritized-pieces/{setlistPieceId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> RemovePrioritizedPiece([FromRoute] Guid id, [FromRoute] Guid setlistPieceId)
        {
            await _appointmentService.RemovePrioritizedPieceAsync(id, setlistPieceId);
            return NoContent();
        }

        /// <summary>
        /// Returns an iCal feed for a given feed token (anonymous, token-based auth)
        /// </summary>
        /// <param name="token">The feed token</param>
        /// <response code="200">Returns the iCal feed</response>
        /// <response code="401">If the token is invalid</response>
        [AllowAnonymous]
        [HttpGet("feed")]
        [Produces("text/calendar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CalendarFeed([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            string calendarData = await _appointmentService.GenerateCalendarFeedAsync(token);
            if (calendarData == null)
            {
                return Unauthorized();
            }

            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            return File(Encoding.UTF8.GetBytes(calendarData), "text/calendar");
        }

        /// <summary>
        /// Gets all feed tokens for the current user
        /// </summary>
        /// <response code="200">Returns a list of feed tokens</response>
        [Authorize]
        [HttpGet("feed/tokens")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CalendarFeedTokenDto>>> GetMyFeedTokens()
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            return Ok(await _appointmentService.GetFeedTokensAsync(_tokenAccessor.UserId, baseUrl));
        }

        /// <summary>
        /// Creates a new feed token for the current user
        /// </summary>
        /// <param name="dto">Feed token configuration</param>
        /// <response code="201">Returns the created feed token</response>
        [Authorize]
        [HttpPost("feed/tokens")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CalendarFeedTokenDto>> CreateFeedToken([FromBody] CreateCalendarFeedTokenDto dto)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            CalendarFeedTokenDto result = await _appointmentService.CreateFeedTokenAsync(_tokenAccessor.UserId, dto, baseUrl);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Deletes (deactivates) a feed token
        /// </summary>
        /// <param name="id">The feed token ID</param>
        /// <response code="204"></response>
        /// <response code="404">If the token was not found</response>
        [Authorize]
        [HttpDelete("feed/tokens/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteFeedToken([FromRoute] Guid id)
        {
            await _appointmentService.DeleteFeedTokenAsync(_tokenAccessor.UserId, id);
            return NoContent();
        }
    }
}
