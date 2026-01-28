using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.StageSetupApplication.Interfaces;
using Orso.Arpa.Application.StageSetupApplication.Model;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    /// <summary>
    /// Controller for managing stage setup positions
    /// </summary>
    [Route("api/stage-setups/{setupId}/positions")]
    public class StageSetupPositionsController : BaseController
    {
        private readonly IStageSetupPositionService _positionService;

        public StageSetupPositionsController(IStageSetupPositionService positionService)
        {
            _positionService = positionService;
        }

        /// <summary>
        /// Gets all positions for a stage setup
        /// </summary>
        /// <param name="setupId">The stage setup ID</param>
        /// <returns>A list of positions</returns>
        /// <response code="200">Returns the list of positions</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastPerformerPolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StageSetupPositionDto>>> Get([FromRoute] Guid setupId)
        {
            return Ok(await _positionService.GetBySetupAsync(setupId));
        }

        /// <summary>
        /// Creates a new position in a stage setup
        /// </summary>
        /// <param name="setupId">The stage setup ID</param>
        /// <param name="createDto">The position data</param>
        /// <returns>The created position</returns>
        /// <response code="201">Returns the created position</response>
        /// <response code="400">If the musician is already positioned</response>
        /// <response code="404">If the stage setup or musician profile could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<StageSetupPositionDto>> Post(
            [FromRoute] Guid setupId,
            [FromBody] StageSetupPositionCreateDto createDto)
        {
            var result = await _positionService.CreateAsync(setupId, createDto);
            return CreatedAtAction(nameof(Get), new { setupId }, result);
        }

        /// <summary>
        /// Updates an existing position
        /// </summary>
        /// <param name="setupId">The stage setup ID</param>
        /// <param name="musicianProfileId">The musician profile ID</param>
        /// <param name="modifyDto">The position data</param>
        /// <response code="204">Successfully updated</response>
        /// <response code="404">If the position could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{musicianProfileId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(
            [FromRoute] Guid setupId,
            [FromRoute] Guid musicianProfileId,
            [FromBody] StageSetupPositionModifyDto modifyDto)
        {
            // We need to find the position ID first
            var positions = await _positionService.GetBySetupAsync(setupId);
            StageSetupPositionDto position = null;
            foreach (var p in positions)
            {
                if (p.MusicianProfileId == musicianProfileId)
                {
                    position = p;
                    break;
                }
            }

            if (position == null)
            {
                return NotFound();
            }

            var bodyDto = new StageSetupPositionModifyBodyDto
            {
                Id = position.Id,
                StageSetupId = setupId,
                PositionX = modifyDto.PositionX,
                PositionY = modifyDto.PositionY,
                Row = modifyDto.Row,
                Stand = modifyDto.Stand
            };

            await _positionService.ModifyAsync(bodyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a position (moves musician back to pool)
        /// </summary>
        /// <param name="setupId">The stage setup ID</param>
        /// <param name="musicianProfileId">The musician profile ID</param>
        /// <response code="204">Successfully deleted</response>
        /// <response code="404">If the position could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("{musicianProfileId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(
            [FromRoute] Guid setupId,
            [FromRoute] Guid musicianProfileId)
        {
            await _positionService.DeleteAsync(setupId, musicianProfileId);
            return NoContent();
        }

        /// <summary>
        /// Bulk updates positions (for drag-drop operations)
        /// </summary>
        /// <param name="setupId">The stage setup ID</param>
        /// <param name="bulkUpdateDto">The positions to update</param>
        /// <returns>The updated positions</returns>
        /// <response code="200">Returns the updated positions</response>
        /// <response code="404">If the stage setup could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<IEnumerable<StageSetupPositionDto>>> BulkUpdate(
            [FromRoute] Guid setupId,
            [FromBody] BulkUpdatePositionsDto bulkUpdateDto)
        {
            var result = await _positionService.BulkUpdateAsync(setupId, bulkUpdateDto);
            return Ok(result);
        }
    }
}
