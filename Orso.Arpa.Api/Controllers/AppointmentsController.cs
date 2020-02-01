using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.ModelBinders;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.AppointmentParticipations;
using Orso.Arpa.Application.Logic.Appointments;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Infrastructure.Authorization;
using static Orso.Arpa.Application.Logic.Appointments.AddProject;

namespace Orso.Arpa.Api.Controllers
{
    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> Get([FromQuery]DateTime? date, [FromQuery]DateRange range)
        {
            return Ok(await _appointmentService.GetAsync(date, range));
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetById(Guid id)
        {
            return Ok(await _appointmentService.GetAsync(id));
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AppointmentDto>> Post([FromBody]Create.Dto appointmentCreateDto)
        {
            AppointmentDto createdAppointment = await _appointmentService.CreateAsync(appointmentCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.Id }, createdAppointment);
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPost("{id}/rooms/{roomId}")]
        public async Task<ActionResult> AddRoom([FromRoute]AddRoom.Dto addRoomDto)
        {
            await _appointmentService.AddRoomAsync(addRoomDto);
            return Ok();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPost("{id}/projects/{projectId}")]
        public async Task<ActionResult> AddProject([FromRoute]Dto addProjectDto)
        {
            await _appointmentService.AddProjectAsync(addProjectDto);
            return Ok();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPost("{id}/sections/{sectionId}")]
        public async Task<ActionResult> AddSection([FromRoute]AddSection.Dto addSectionDto)
        {
            await _appointmentService.AddSectionAsync(addSectionDto);
            return Ok();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPut("{id}/venue/set/{venueId}")]
        public async Task<ActionResult> SetVenue([FromRoute]SetVenue.Dto setVenueDto)
        {
            await _appointmentService.SetVenueAsync(setVenueDto);
            return Ok();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put([FromBody][ModelBinder(typeof(ModifyDtoModelBinder<Modify.Dto>))]Modify.Dto appointmentModifyDto)
        {
            await _appointmentService.ModifyAsync(appointmentModifyDto);

            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpDelete("{id}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RemoveRoom([FromRoute]RemoveRoom.Dto removeRoomDto)
        {
            await _appointmentService.RemoveRoomAsync(removeRoomDto);
            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpDelete("{id}/sections/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RemoveSection([FromRoute]RemoveSection.Dto removeSectionDto)
        {
            await _appointmentService.RemoveSectionAsync(removeSectionDto);
            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpDelete("{id}/projects/{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RemoveProject([FromRoute]RemoveProject.Dto removeProjectDto)
        {
            await _appointmentService.RemoveProjectAsync(removeProjectDto);
            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _appointmentService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPut("{id}/dates/set")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SetDates(
            [FromBody][ModelBinder(typeof(ModifyDtoModelBinder<SetDates.Dto>))]SetDates.Dto setDatesDto)
        {
            await _appointmentService.SetDatesAsync(setDatesDto);
            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPut("{id}/participations/{personId}/result/{resultId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SetParticipationResult([FromRoute]SetResult.Dto setParticipationResult)
        {
            await _appointmentService.SetParticipationResultAsync(setParticipationResult);
            return NoContent();
        }
    }
}
