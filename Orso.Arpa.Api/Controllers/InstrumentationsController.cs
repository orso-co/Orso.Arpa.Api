using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.InstrumentationApplication.Model;
using Orso.Arpa.Application.InstrumentationApplication.Services;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers
{
    public class InstrumentationsController : BaseController
    {
        private readonly IInstrumentationService _instrumentationService;

        public InstrumentationsController(IInstrumentationService instrumentationService)
        {
            _instrumentationService = instrumentationService;
        }

        /// <summary>
        /// Gets all instrumentations.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InstrumentationDto>>> Get([FromQuery] bool includeTemplates = true)
        {
            return Ok(await _instrumentationService.GetAsync(includeTemplates));
        }

        /// <summary>
        /// Gets an instrumentation by id.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InstrumentationDto>> GetById(Guid id)
        {
            return Ok(await _instrumentationService.GetByIdAsync(id));
        }

        /// <summary>
        /// Creates a new instrumentation.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InstrumentationDto>> Post([FromBody] InstrumentationCreateDto createDto)
        {
            InstrumentationDto result = await _instrumentationService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing instrumentation.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(InstrumentationModifyDto modifyDto)
        {
            await _instrumentationService.ModifyAsync(modifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an instrumentation.
        /// </summary>
        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _instrumentationService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Deep-copies an instrumentation (e.g. template to project).
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/copy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InstrumentationDto>> Copy(Guid id, [FromBody] InstrumentationCopyDto copyDto)
        {
            InstrumentationDto result = await _instrumentationService.CopyAsync(id, copyDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Gets the IST/SOLL status for an instrumentation.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InstrumentationStatusDto>> GetStatus(Guid id)
        {
            return Ok(await _instrumentationService.GetStatusAsync(id));
        }

        // --- Position endpoints ---

        /// <summary>
        /// Adds a position to an instrumentation.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/positions")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InstrumentationPositionDto>> AddPosition(Guid id, [FromBody] InstrumentationPositionCreateDto dto)
        {
            InstrumentationPositionDto result = await _instrumentationService.AddPositionAsync(id, dto);
            return CreatedAtAction(nameof(GetById), new { id }, result);
        }

        /// <summary>
        /// Modifies a position in an instrumentation.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/positions/{posId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ModifyPosition(Guid id, Guid posId, [FromBody] InstrumentationPositionModifyDto dto)
        {
            await _instrumentationService.ModifyPositionAsync(id, posId, dto);
            return NoContent();
        }

        /// <summary>
        /// Removes a position from an instrumentation.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/positions/{posId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemovePosition(Guid id, Guid posId)
        {
            await _instrumentationService.RemovePositionAsync(id, posId);
            return NoContent();
        }

        /// <summary>
        /// Reorders positions in an instrumentation.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/positions/reorder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReorderPositions(Guid id, [FromBody] List<Guid> orderedPositionIds)
        {
            await _instrumentationService.ReorderPositionsAsync(id, orderedPositionIds);
            return NoContent();
        }

        // --- Doubling endpoints ---

        /// <summary>
        /// Adds a doubling instrument to a position.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/positions/{posId}/doublings")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InstrumentationPositionDoublingDto>> AddDoubling(Guid id, Guid posId, [FromBody] AddDoublingDto dto)
        {
            InstrumentationPositionDoublingDto result = await _instrumentationService.AddDoublingAsync(posId, dto.SectionId);
            return CreatedAtAction(nameof(GetById), new { id }, result);
        }

        /// <summary>
        /// Removes a doubling instrument from a position.
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/positions/{posId}/doublings/{dblId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveDoubling(Guid id, Guid posId, Guid dblId)
        {
            await _instrumentationService.RemoveDoublingAsync(posId, dblId);
            return NoContent();
        }
    }

    public class AddDoublingDto
    {
        public Guid SectionId { get; set; }
    }
}
