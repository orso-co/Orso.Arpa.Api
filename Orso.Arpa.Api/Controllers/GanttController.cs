using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.TodoApplication.Interfaces;
using Orso.Arpa.Application.TodoApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/gantt")]
    public class GanttController : BaseController
    {
        private readonly ITodoService _todoService;

        public GanttController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Gets all tasks with dependencies for Gantt view
        /// </summary>
        [HttpGet("tasks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GanttTaskDto>>> GetTasks([FromQuery] Guid? projectId)
        {
            return Ok(await _todoService.GetGanttTasksAsync(projectId));
        }

        /// <summary>
        /// Updates start/end dates of a task (drag &amp; drop)
        /// </summary>
        [HttpPatch("tasks/{id}/dates")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PatchDates([FromRoute] Guid id, [FromBody] GanttDateUpdateDto dto)
        {
            await _todoService.UpdateGanttDatesAsync(id, dto.StartDate, dto.EndDate);
            return NoContent();
        }

        /// <summary>
        /// Creates a dependency between tasks
        /// </summary>
        [HttpPost("dependencies")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> PostDependency([FromBody] GanttDependencyCreateDto dto)
        {
            await _todoService.CreateDependencyAsync(dto);
            return Created(string.Empty, null);
        }

        /// <summary>
        /// Removes a dependency between tasks
        /// </summary>
        [HttpDelete("dependencies/{depId}/{onId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteDependency([FromRoute] Guid depId, [FromRoute] Guid onId)
        {
            await _todoService.RemoveDependencyAsync(depId, onId);
            return NoContent();
        }

        /// <summary>
        /// Updates the sort order of a task
        /// </summary>
        [HttpPatch("tasks/{id}/sort-order")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PatchSortOrder([FromRoute] Guid id, [FromBody] GanttSortOrderUpdateDto dto)
        {
            await _todoService.UpdateSortOrderAsync(id, dto.SortOrder);
            return NoContent();
        }
    }
}
