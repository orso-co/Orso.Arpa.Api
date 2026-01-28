using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.StageSetupApplication.Interfaces;
using Orso.Arpa.Application.StageSetupApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    /// <summary>
    /// Controller for managing stage setups (Aufstellungen)
    /// </summary>
    [Route("api/projects/{projectId}/stage-setups")]
    public class StageSetupsController : BaseController
    {
        private readonly IStageSetupService _stageSetupService;
        private readonly ITokenAccessor _tokenAccessor;

        public StageSetupsController(
            IStageSetupService stageSetupService,
            ITokenAccessor tokenAccessor)
        {
            _stageSetupService = stageSetupService;
            _tokenAccessor = tokenAccessor;
        }

        /// <summary>
        /// Gets all stage setups for a project
        /// </summary>
        /// <param name="projectId">The project ID</param>
        /// <returns>A list of stage setups</returns>
        /// <response code="200">Returns the list of stage setups</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastPerformerPolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StageSetupDto>>> Get([FromRoute] Guid projectId)
        {
            // Staff can see all setups, performers only visible ones
            bool isStaff = _tokenAccessor.GetUserRoles().Contains("Staff") || _tokenAccessor.GetUserRoles().Contains("Admin");
            return Ok(await _stageSetupService.GetByProjectAsync(projectId, includeHidden: isStaff));
        }

        /// <summary>
        /// Gets a single stage setup by ID
        /// </summary>
        /// <param name="projectId">The project ID</param>
        /// <param name="id">The stage setup ID</param>
        /// <returns>The stage setup</returns>
        /// <response code="200">Returns the stage setup</response>
        /// <response code="404">If the stage setup could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastPerformerPolicy)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StageSetupDto>> GetById([FromRoute] Guid projectId, [FromRoute] Guid id)
        {
            var result = await _stageSetupService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Creates a new stage setup with PDF upload
        /// </summary>
        /// <param name="projectId">The project ID</param>
        /// <param name="createDto">The stage setup data</param>
        /// <param name="file">The PDF file</param>
        /// <returns>The created stage setup</returns>
        /// <response code="201">Returns the created stage setup</response>
        /// <response code="400">If the file is missing or invalid</response>
        /// <response code="404">If the project could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<StageSetupDto>> Post(
            [FromRoute] Guid projectId,
            [FromForm] StageSetupCreateDto createDto,
            IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("A PDF file is required");
            }

            if (file.ContentType != "application/pdf")
            {
                return BadRequest("Only PDF files are allowed");
            }

            using var stream = file.OpenReadStream();
            var result = await _stageSetupService.CreateAsync(
                projectId,
                createDto,
                stream,
                file.FileName,
                file.ContentType);

            return CreatedAtAction(nameof(GetById), new { projectId, id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing stage setup
        /// </summary>
        /// <param name="projectId">The project ID</param>
        /// <param name="id">The stage setup ID</param>
        /// <param name="modifyDto">The stage setup data</param>
        /// <response code="204">Successfully updated</response>
        /// <response code="404">If the stage setup could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(
            [FromRoute] Guid projectId,
            [FromRoute] Guid id,
            [FromBody] StageSetupModifyDto modifyDto)
        {
            var bodyDto = new StageSetupModifyBodyDto
            {
                Id = id,
                ProjectId = projectId,
                Name = modifyDto.Name,
                CanvasWidth = modifyDto.CanvasWidth,
                CanvasHeight = modifyDto.CanvasHeight,
                IsActive = modifyDto.IsActive,
                IsVisibleToPerformers = modifyDto.IsVisibleToPerformers
            };

            await _stageSetupService.ModifyAsync(bodyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing stage setup
        /// </summary>
        /// <param name="projectId">The project ID</param>
        /// <param name="id">The stage setup ID</param>
        /// <response code="204">Successfully deleted</response>
        /// <response code="404">If the stage setup could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid projectId, [FromRoute] Guid id)
        {
            await _stageSetupService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Downloads the PDF file for a stage setup
        /// </summary>
        /// <param name="projectId">The project ID</param>
        /// <param name="id">The stage setup ID</param>
        /// <returns>The PDF file</returns>
        /// <response code="200">Returns the PDF file</response>
        /// <response code="404">If the stage setup or file could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastPerformerPolicy)]
        [HttpGet("{id}/file")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetFile([FromRoute] Guid projectId, [FromRoute] Guid id)
        {
            var (stream, fileName, contentType) = await _stageSetupService.GetFileAsync(id);
            if (stream == null)
            {
                return NotFound();
            }

            return File(stream, contentType, fileName);
        }

        /// <summary>
        /// Replaces the PDF file for a stage setup
        /// </summary>
        /// <param name="projectId">The project ID</param>
        /// <param name="id">The stage setup ID</param>
        /// <param name="file">The new PDF file</param>
        /// <response code="204">Successfully replaced</response>
        /// <response code="400">If the file is missing or invalid</response>
        /// <response code="404">If the stage setup could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{id}/file")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ReplaceFile(
            [FromRoute] Guid projectId,
            [FromRoute] Guid id,
            IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("A PDF file is required");
            }

            if (file.ContentType != "application/pdf")
            {
                return BadRequest("Only PDF files are allowed");
            }

            using var stream = file.OpenReadStream();
            await _stageSetupService.ReplaceFileAsync(id, stream, file.FileName, file.ContentType);
            return NoContent();
        }

        /// <summary>
        /// Activates a stage setup (deactivates all others for the project)
        /// </summary>
        /// <param name="projectId">The project ID</param>
        /// <param name="id">The stage setup ID</param>
        /// <response code="204">Successfully activated</response>
        /// <response code="404">If the stage setup could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{id}/activate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Activate([FromRoute] Guid projectId, [FromRoute] Guid id)
        {
            await _stageSetupService.ActivateAsync(projectId, id);
            return NoContent();
        }

        /// <summary>
        /// Sets the visibility of a stage setup for performers
        /// </summary>
        /// <param name="projectId">The project ID</param>
        /// <param name="id">The stage setup ID</param>
        /// <param name="isVisible">Whether the setup should be visible to performers</param>
        /// <response code="204">Successfully updated</response>
        /// <response code="404">If the stage setup could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{id}/visibility")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> SetVisibility(
            [FromRoute] Guid projectId,
            [FromRoute] Guid id,
            [FromQuery] bool isVisible)
        {
            await _stageSetupService.SetVisibilityAsync(projectId, id, isVisible);
            return NoContent();
        }
    }
}
