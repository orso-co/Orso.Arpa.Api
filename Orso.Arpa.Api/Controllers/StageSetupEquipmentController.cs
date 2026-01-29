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
    /// Controller for managing stage setup equipment (chairs, tables, piano, etc.)
    /// </summary>
    [Route("api/stage-setups/{setupId}/equipment")]
    public class StageSetupEquipmentController : BaseController
    {
        private readonly IStageSetupEquipmentService _equipmentService;

        public StageSetupEquipmentController(IStageSetupEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        /// <summary>
        /// Gets all equipment for a stage setup
        /// </summary>
        /// <param name="setupId">The stage setup ID</param>
        /// <returns>A list of equipment</returns>
        /// <response code="200">Returns the list of equipment</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastPerformerPolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StageSetupEquipmentDto>>> Get([FromRoute] Guid setupId)
        {
            return Ok(await _equipmentService.GetBySetupAsync(setupId));
        }

        /// <summary>
        /// Creates a new equipment item in a stage setup
        /// </summary>
        /// <param name="setupId">The stage setup ID</param>
        /// <param name="createDto">The equipment data</param>
        /// <returns>The created equipment</returns>
        /// <response code="201">Returns the created equipment</response>
        /// <response code="404">If the stage setup could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<StageSetupEquipmentDto>> Post(
            [FromRoute] Guid setupId,
            [FromBody] StageSetupEquipmentCreateDto createDto)
        {
            var result = await _equipmentService.CreateAsync(setupId, createDto);
            return CreatedAtAction(nameof(Get), new { setupId }, result);
        }

        /// <summary>
        /// Updates an existing equipment item
        /// </summary>
        /// <param name="setupId">The stage setup ID</param>
        /// <param name="id">The equipment ID</param>
        /// <param name="modifyDto">The equipment data</param>
        /// <response code="204">Successfully updated</response>
        /// <response code="404">If the equipment could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(
            [FromRoute] Guid setupId,
            [FromRoute] Guid id,
            [FromBody] StageSetupEquipmentModifyDto modifyDto)
        {
            await _equipmentService.ModifyAsync(id, modifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an equipment item
        /// </summary>
        /// <param name="setupId">The stage setup ID</param>
        /// <param name="id">The equipment ID</param>
        /// <response code="204">Successfully deleted</response>
        /// <response code="404">If the equipment could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(
            [FromRoute] Guid setupId,
            [FromRoute] Guid id)
        {
            await _equipmentService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Bulk updates equipment (for drag-drop operations)
        /// </summary>
        /// <param name="setupId">The stage setup ID</param>
        /// <param name="bulkUpdateDto">The equipment to update</param>
        /// <returns>The updated equipment</returns>
        /// <response code="200">Returns the updated equipment</response>
        /// <response code="404">If the stage setup could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<IEnumerable<StageSetupEquipmentDto>>> BulkUpdate(
            [FromRoute] Guid setupId,
            [FromBody] BulkUpdateEquipmentDto bulkUpdateDto)
        {
            var result = await _equipmentService.BulkUpdateAsync(setupId, bulkUpdateDto);
            return Ok(result);
        }
    }
}
