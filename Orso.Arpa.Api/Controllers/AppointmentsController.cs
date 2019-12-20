using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.ModelBinders;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Appointments;
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

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> Get([FromQuery]DateTime? date, [FromQuery]DateRange range)
        {
            return Ok(await _appointmentService.GetAsync(date, range));
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> Get(Guid id)
        {
            return Ok(await _appointmentService.GetAsync(id));
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Post([FromBody]AppointmentCreateDto appointmentCreateDto)
        {
            return Ok(await _appointmentService.CreateAsync(appointmentCreateDto));
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPost("{id}/rooms/{roomId}")]
        public async Task<ActionResult> AddRoom(Guid id, Guid roomId)
        {
            await _appointmentService.AddRoomAsync(id, roomId);
            return Ok();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPost("{id}/projects/{projectId}")]
        public async Task<ActionResult> AddProject(Guid id, Guid projectId)
        {
            await _appointmentService.AddProjectAsync(id, projectId);
            return Ok();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPost("{id}/registers/{registerId}")]
        public async Task<ActionResult> AddRegister(Guid id, Guid registerId)
        {
            await _appointmentService.AddRegisterAsync(id, registerId);
            return Ok();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(
            [FromBody][ModelBinder(typeof(ModifyDtoModelBinder<AppointmentModifyDto>))]AppointmentModifyDto appointmentModifyDto)
        {
            await _appointmentService.ModifyAsync(appointmentModifyDto);

            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpDelete("{id}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RemoveRoom(Guid id, Guid roomId)
        {
            await _appointmentService.RemoveRoomAsync(id, roomId);
            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpDelete("{id}/registers/{registerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RemoveRegister(Guid id, Guid registerId)
        {
            await _appointmentService.RemoveRegisterAsync(id, registerId);
            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpDelete("{id}/projects/{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RemoveProject(Guid id, Guid projectId)
        {
            await _appointmentService.RemoveProjectAsync(id, projectId);
            return NoContent();
        }
    }
}
