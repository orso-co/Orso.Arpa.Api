using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.RoomApplication.Interfaces;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class RoomEquipmentsController : BaseController
    {
        private readonly IRoomEquipmentService _roomEquipmentService;


        public RoomEquipmentsController(IRoomEquipmentService roomEquipmentService)
        {
            _roomEquipmentService = roomEquipmentService;
        }

        /// <summary>
        /// Gets a room equipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried room equipment</returns>
        /// <response code="200"></response>
        /// <response code="404">If no room equipment could be found for the supplied id</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomEquipmentDto>> GetById([FromRoute] Guid id)
        {
            return await _roomEquipmentService.GetByIdAsync(id);
        }

        /// <summary>
        /// Updates an existing room equipment
        /// </summary>
        /// <param name="roomEquipmentModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(RoomEquipmentModifyDto roomEquipmentModifyDto)
        {
            await _roomEquipmentService.ModifyAsync(roomEquipmentModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes existing room equipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _roomEquipmentService.DeleteAsync(id);
            return NoContent();
        }
    }
}