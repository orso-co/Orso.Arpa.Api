using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Api.ModelBinding;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Gets the projects including completed if requested
        /// </summary>
        /// <param name="includeCompleted"></param>
        /// <returns>A list of projects</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> Get([FromQuery] bool includeCompleted = false)
        {
            return Ok(await _projectService.GetAsync(includeCompleted));
        }

        /// <summary>
        /// Gets a project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried project</returns>
        /// <response code="200"></response>
        /// <response code="404">If no project could be found for the supplied id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _projectService.GetByIdAsync(id));
        }

        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="projectCreateDto"></param>
        /// <returns>The created project</returns>
        /// <response code="201">Returns the created project</response>
        /// <response code="400">If dto is not valid</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectDto>> Post([FromBody] ProjectCreateDto projectCreateDto)
        {
            ProjectDto createdProject = await _projectService.CreateAsync(projectCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdProject.Id }, createdProject);
        }

        /// <summary>
        /// Modifies existing project
        /// </summary>
        /// <param name="projectModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or if project could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        [SwaggerFromRouteProperty(nameof(ProjectModifyDto.Id))]
        public async Task<ActionResult> Put([FromBodyAndRoute] ProjectModifyDto projectModifyDto)
        {
            await _projectService.ModifyAsync(projectModifyDto);

            return NoContent();
        }

        /// <summary>
        /// Deletes existing project by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"></response>
        /// <response code="400">If project could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _projectService.DeleteAsync(id);
            return NoContent();
        }

    }
}
