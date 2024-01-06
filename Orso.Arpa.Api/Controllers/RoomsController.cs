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
    public class RoomsController : BaseController
    {
        private readonly IRoomService _roomService;
        private readonly IRoomSectionService _roomSectionService;
        private readonly IRoomEquipmentService _roomEquipmentService;


        public RoomsController(IRoomService roomService, IRoomSectionService roomSectionService, IRoomEquipmentService roomEquipmentService)
        {
            _roomService = roomService;
            _roomSectionService = roomSectionService;
            _roomEquipmentService = roomEquipmentService;

        }

        /// <summary>
        /// Gets a room by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried room</returns>
        /// <response code="200"></response>
        /// <response code="404">If no room could be found for the supplied id</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomDto>> GetById([FromRoute] Guid id)
        {
            return await _roomService.GetByIdAsync(id);
        }

        /// <summary>
        /// Updates an existing room
        /// </summary>
        /// <param name="roomModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(RoomModifyDto roomModifyDto)
        {
            await _roomService.ModifyAsync(roomModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes existing room by id
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
            await _roomService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Adds a new instrument to an existing room
        /// </summary>
        /// <param name="roomSectionCreateDto"></param>
        /// <returns>The created room section</returns>
        /// <response code="201"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/instruments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<RoomSectionDto>> AddRoomSection(RoomSectionCreateDto roomSectionCreateDto)
        {
            RoomSectionDto createdDto = await _roomSectionService.CreateAsync(roomSectionCreateDto);
            return CreatedAtAction(nameof(RoomSectionsController.GetById), "RoomSections", new { id = createdDto.Id }, createdDto);
        }

        /// <summary>
        /// Adds a new equipment to an existing room
        /// </summary>
        /// <param name="roomEquipmentCreateDto"></param>
        /// <returns>The created room equipment</returns>
        /// <response code="201"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/equipments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<RoomEquipmentDto>> AddRoomEquipment(RoomEquipmentCreateDto roomEquipmentCreateDto)
        {
            RoomEquipmentDto createdDto = await _roomEquipmentService.CreateAsync(roomEquipmentCreateDto);
            return CreatedAtAction(nameof(RoomEquipmentsController.GetById), "RoomEquipments", new { id = createdDto.Id }, createdDto);
        }
    }
}