using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Api.ModelBinding;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Queries a list of appointments dependent on the given date and date range
        /// </summary>
        /// <param name="date"></param>
        /// <param name="range"></param>
        /// <returns>A list of appointments</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> Get([FromQuery] DateTime? date, [FromQuery] DateRange range)
        {
            return Ok(await _appointmentService.GetAsync(date, range));
        }

        /// <summary>
        /// Gets an appointment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried appointment</returns>
        /// <response code="200"></response>
        /// <response code="404">If no appointment could be found for the supplied id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _appointmentService.GetByIdAsync(id));
        }

        /// <summary>
        /// Creates a new appointment
        /// </summary>
        /// <param name="appointmentCreateDto"></param>
        /// <returns>The created appointment</returns>
        /// <response code="201">Returns the created appointment</response>
        /// <response code="400">If dto is not valid</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppointmentDto>> Post([FromBody] AppointmentCreateDto appointmentCreateDto)
        {
            AppointmentDto createdAppointment = await _appointmentService.CreateAsync(appointmentCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.Id }, createdAppointment);
        }

        /// <summary>
        /// Adds a room to an existing appointment
        /// </summary>
        /// <param name="addRoomDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or if room or appointment could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddRoom([FromRoute] AppointmentAddRoomDto addRoomDto)
        {
            await _appointmentService.AddRoomAsync(addRoomDto);
            return NoContent();
        }

        /// <summary>
        /// Adds a project to an existing appointment
        /// </summary>
        /// <param name="addProjectDto"></param>
        /// <response code="200"></response>
        /// <response code="400">If dto is not valid or if project or appointment could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/projects/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppointmentDto>> AddProject([FromRoute] AppointmentAddProjectDto addProjectDto)
        {
            return await _appointmentService.AddProjectAsync(addProjectDto);
        }

        /// <summary>
        /// Adds a section to an existing appointment
        /// </summary>
        /// <param name="addSectionDto"></param>
        /// <response code="200"></response>
        /// <response code="400">If dto is not valid or if section or appointment could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/sections/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppointmentDto>> AddSection([FromRoute] AppointmentAddSectionDto addSectionDto)
        {
            return await _appointmentService.AddSectionAsync(addSectionDto);
        }

        /// <summary>
        /// Sets the venue of an existing appointment
        /// </summary>
        /// <param name="setVenueDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or if venue or appointment could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/venue/set/{venueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
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
        /// <response code="400">If dto is not valid or if appointment could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        [SwaggerFromRouteProperty(nameof(AppointmentModifyDto.Id))]
        public async Task<ActionResult> Put([FromBodyAndRoute] AppointmentModifyDto appointmentModifyDto)
        {
            await _appointmentService.ModifyAsync(appointmentModifyDto);

            return NoContent();
        }

        /// <summary>
        /// Removes room from existing appointment
        /// </summary>
        /// <param name="removeRoomDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveRoom([FromRoute] AppointmentRemoveRoomDto removeRoomDto)
        {
            await _appointmentService.RemoveRoomAsync(removeRoomDto);
            return NoContent();
        }

        /// <summary>
        /// Removes section from existing appointment
        /// </summary>
        /// <param name="removeSectionDto"></param>
        /// <response code="200"></response>
        /// <response code="400">If dto is not valid</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/sections/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppointmentDto>> RemoveSection([FromRoute] AppointmentRemoveSectionDto removeSectionDto)
        {
            return await _appointmentService.RemoveSectionAsync(removeSectionDto);
        }

        /// <summary>
        /// Removes project from existing appointment
        /// </summary>
        /// <param name="removeProjectDto"></param>
        /// <response code="200"></response>
        /// <response code="400">If dto is not valid</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/projects/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppointmentDto>> RemoveProject([FromRoute] AppointmentRemoveProjectDto removeProjectDto)
        {
            return await _appointmentService.RemoveProjectAsync(removeProjectDto);
        }

        /// <summary>
        /// Deletes existing appointment by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"></response>
        /// <response code="400">If appointment could not be found</response>
        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
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
        /// <response code="400">If dto is not valid</response>
        /// <response code="400">If appointment could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/dates/set")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        [SwaggerFromRouteProperty(nameof(AppointmentSetDatesDto.Id))]
        public async Task<ActionResult<AppointmentDto>> SetDates([FromBodyAndRoute] AppointmentSetDatesDto setDatesDto)
        {
            return await _appointmentService.SetDatesAsync(setDatesDto);
        }

        /// <summary>
        /// Sets the result of an appointment participation
        /// </summary>
        /// <param name="setParticipationResult"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or if appointment or participation or result value could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/participations/{personId}/result/{resultId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SetParticipationResult([FromRoute] AppointmentParticipationSetResultDto setParticipationResult)
        {
            await _appointmentService.SetParticipationResultAsync(setParticipationResult);
            return NoContent();
        }
    }
}
