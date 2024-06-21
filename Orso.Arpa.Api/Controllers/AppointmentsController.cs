using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AppointmentApplication.Interfaces;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers;

public class AppointmentsController : BaseController
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    /// <summary>
    ///     Queries a list of appointments dependent on the given date and date range
    /// </summary>
    /// <param name="date"></param>
    /// <param name="range"></param>
    /// <returns>A list of appointments</returns>
    /// <response code="200"></response>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AppointmentListDto>>> Get([FromQuery] DateTime? date,
        [FromQuery] DateRange range)
    {
        return Ok(await _appointmentService.GetAsync(date, range));
    }

    /// <summary>
    ///     Gets an appointment by id
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
    public async Task<ActionResult<AppointmentDto>> GetById([FromRoute] Guid id,
        [FromQuery] bool includeParticipations = true)
    {
        return Ok(await _appointmentService.GetByIdAsync(id, includeParticipations));
    }

    /// <summary>
    ///     Creates a new appointment
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
    public async Task<ActionResult<AppointmentDto>> Post(
        [FromBody] AppointmentCreateDto appointmentCreateDto)
    {
        AppointmentDto createdAppointment =
            await _appointmentService.CreateAsync(appointmentCreateDto);
        return CreatedAtAction(nameof(GetById), new { id = createdAppointment.Id },
            createdAppointment);
    }

    /// <summary>
    ///     Adds a room to an existing appointment
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
    ///     Adds a project to an existing appointment
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
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<AppointmentDto>> AddProject(
        [FromRoute] AppointmentAddProjectDto addProjectDto,
        [FromQuery] bool includeParticipations = true)
    {
        return await _appointmentService.AddProjectAsync(addProjectDto, includeParticipations);
    }

    /// <summary>
    ///     Adds a section to an existing appointment
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
    public async Task<ActionResult<AppointmentDto>> AddSection(
        [FromRoute] AppointmentAddSectionDto addSectionDto,
        [FromQuery] bool includeParticipations = true)
    {
        return await _appointmentService.AddSectionAsync(addSectionDto, includeParticipations);
    }

    /// <summary>
    ///     Sends an appointment changed notification to all participants of the appointment
    /// </summary>
    /// <param name="sendAppointmentChangedNotificationDto"></param>
    /// <response code="204"></response>
    /// <response code="404">If entity could not be found</response>
    /// <response code="422">If validation fails</response>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("{id}/notification")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<AppointmentDto>> SendAppointmentChangedNotification(
        SendAppointmentChangedNotificationDto sendAppointmentChangedNotificationDto)
    {
        await _appointmentService.SendAppointmentChangedNotificationAsync(
            sendAppointmentChangedNotificationDto);
        return NoContent();
    }

    /// <summary>
    ///     Sets the venue of an existing appointment
    /// </summary>
    /// <param name="setVenueDto"></param>
    /// <response code="204"></response>
    /// <response code="404">If entity could not be found</response>
    /// <response code="422">If validation fails</response>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("{id}/venue/set/{venueId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> SetVenue([FromRoute] AppointmentSetVenueDto setVenueDto)
    {
        await _appointmentService.SetVenueAsync(setVenueDto);
        return NoContent();
    }

    /// <summary>
    ///     Modifies existing appointment
    /// </summary>
    /// <param name="appointmentModifyDto"></param>
    /// <response code="204"></response>
    /// <response code="404">If entity could not be found</response>
    /// <response code="422">If validation fails</response>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Put(AppointmentModifyDto appointmentModifyDto)
    {
        await _appointmentService.ModifyAsync(appointmentModifyDto);

        return NoContent();
    }

    /// <summary>
    ///     Removes room from existing appointment
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
    public async Task<ActionResult> RemoveRoom([FromRoute] AppointmentRemoveRoomDto removeRoomDto)
    {
        await _appointmentService.RemoveRoomAsync(removeRoomDto);
        return NoContent();
    }

    /// <summary>
    ///     Removes section from existing appointment
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
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<AppointmentDto>> RemoveSection(
        [FromRoute] AppointmentRemoveSectionDto removeSectionDto,
        [FromQuery] bool includeParticipations = true)
    {
        return await _appointmentService.RemoveSectionAsync(removeSectionDto,
            includeParticipations);
    }

    /// <summary>
    ///     Removes project from existing appointment
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
    public async Task<ActionResult<AppointmentDto>> RemoveProject(
        [FromRoute] AppointmentRemoveProjectDto removeProjectDto,
        [FromQuery] bool includeParticipations = true)
    {
        return await _appointmentService.RemoveProjectAsync(removeProjectDto,
            includeParticipations);
    }

    /// <summary>
    ///     Deletes existing appointment by id
    /// </summary>
    /// <param name="id"></param>
    /// <response code="204"></response>
    /// <response code="404">If entity could not be found</response>
    /// <response code="422">If validation fails</response>
    [Authorize(Roles = RoleNames.Admin)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _appointmentService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    ///     Sets start and end time of an existing appointment
    /// </summary>
    /// <param name="setDatesDto"></param>
    /// <response code="200"></response>
    /// <response code="404">If entity could not be found</response>
    /// <response code="422">If validation fails</response>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("{id}/dates/set")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<AppointmentDto>> SetDates(AppointmentSetDatesDto setDatesDto)
    {
        return await _appointmentService.SetDatesAsync(setDatesDto);
    }

    /// <summary>
    ///     Sets the result of an appointment participation
    /// </summary>
    /// <param name="setParticipationResult"></param>
    /// <response code="204"></response>
    /// <response code="404">If entity could not be found</response>
    /// <response code="422">If validation fails</response>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("{id}/participations/{personId}/result")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> SetParticipationResult(
        AppointmentParticipationSetResultDto setParticipationResult)
    {
        await _appointmentService.SetParticipationResultAsync(setParticipationResult);
        return NoContent();
    }

    /// <summary>
    ///     Sets the prediction of an appointment participation
    /// </summary>
    /// <param name="setParticipationPrediction"></param>
    /// <response code="204"></response>
    /// <response code="404">If entity could not be found</response>
    /// <response code="422">If validation fails</response>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("{id}/participations/{personId}/prediction")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> SetParticipationPrediction(
        AppointmentParticipationSetPredictionDto setParticipationPrediction)
    {
        await _appointmentService.SetParticipationPredictionAsync(setParticipationPrediction);
        return NoContent();
    }

    /// <summary>
    ///     Creates endpoint for export to ics-file
    /// </summary>
    /// <response code="200"></response>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet("export")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ExportToIcs()
    {
        string serializedCalendar = await _appointmentService.ExportAppointmentsToIcsAsync();

        // Return the serialized calendar data as a file download
        return File(Encoding.UTF8.GetBytes(serializedCalendar), "text/calendar",
            "appointments.ics");
    }
}
