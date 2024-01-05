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
    public class RoomSectionsController : BaseController
    {
        private readonly IRoomSectionService _roomSectionService;


        public RoomSectionsController(IRoomSectionService roomSectionService)
        {
            _roomSectionService = roomSectionService;
        }

        /// <summary>
        /// Gets a room section by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried room section</returns>
        /// <response code="200"></response>
        /// <response code="404">If no room section could be found for the supplied id</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomSectionDto>> GetById([FromRoute] Guid id)
        {
            return await _roomSectionService.GetByIdAsync(id);
        }

        /// <summary>
        /// Updates an existing room section
        /// </summary>
        /// <param name="roomSectionModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(RoomSectionModifyDto roomSectionModifyDto)
        {
            await _roomSectionService.ModifyAsync(roomSectionModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes existing room section by id
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
            await _roomSectionService.DeleteAsync(id);
            return NoContent();
        }
    }
}