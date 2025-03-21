using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.RoomApplication.Interfaces;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Application.VenueApplication.Interfaces;
using Orso.Arpa.Application.VenueApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class VenuesController : BaseController
    {
        private readonly IVenueService _venueService;
        private readonly IRoomService _roomService;


        public VenuesController(IVenueService venueService, IRoomService roomService)
        {
            _venueService = venueService;
            _roomService = roomService;

        }

        /// <summary>
        /// Gets all venues
        /// </summary>
        /// <returns>A list of venues</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VenueDto>>> Get()
        {
            return Ok(await _venueService.GetAsync());
        }

        /// <summary>
        /// Gets venue by id
        /// </summary>
        /// <returns>A venue</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VenueDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _venueService.GetByIdAsync(id));
        }

        /// <summary>
        /// Gets the rooms of an existing venue
        /// </summary>
        /// <returns>A list of rooms</returns>
        /// <response code="200"></response>
        /// <response code="404">If venue could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}/rooms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms([FromRoute] Guid id)
        {
            return Ok(await _venueService.GetRoomsAsync(id));
        }

        /// <summary>
        /// Creates a new venue
        /// </summary>
        /// <param name="venueCreateDto"></param>
        /// <returns>The created venue</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<VenueDto>> Post([FromBody] VenueCreateDto venueCreateDto)
        {
            return Ok(await _venueService.CreateAsync(venueCreateDto));
        }

        /// <summary>
        /// Adds a new room to an existing venue
        /// </summary>
        /// <param name="roomCreateDto"></param>
        /// <returns>The created room</returns>
        /// <response code="201"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/rooms")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<VenueDto>> AddRoom(RoomCreateDto roomCreateDto)
        {
            RoomDto createdDto = await _roomService.CreateAsync(roomCreateDto);
            return CreatedAtAction(nameof(RoomsController.GetById), "Rooms", new { id = createdDto.Id }, createdDto);
        }

        /// <summary>
        /// Updates an existing venue
        /// </summary>
        /// <param name="venueModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(VenueModifyDto venueModifyDto)
        {
            await _venueService.ModifyAsync(venueModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing venue
        /// </summary>
        /// <param name="id">the venue id</param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _venueService.DeleteAsync(id);
            return NoContent();
        }
    }
}
