using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.ProjectApplication.Interfaces;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Application.UrlApplication.Interfaces;
using Orso.Arpa.Application.UrlApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IUrlService _urlService;

        public ProjectsController(IProjectService projectService, IUrlService urlService)
        {
            _projectService = projectService;
            _urlService = urlService;
        }

        /// <summary>
        /// Gets the projects including completed if requested
        /// </summary>
        /// <param name="includeCompleted"></param>
        /// <returns>A list of projects</returns>
        /// <response code="200"></response>
        [Authorize(Roles = RoleNames.Staff)]
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
        /// <response code="404">If entity could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
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
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<ProjectDto>> Post([FromBody] ProjectCreateDto projectCreateDto)
        {
            ProjectDto createdProject = await _projectService.CreateAsync(projectCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdProject.Id }, createdProject);
        }

        /// <summary>
        /// Adds a new Url to an existing project
        /// </summary>
        /// <param name="urlCreateDto"></param>
        /// <response code="201"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/urls")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<UrlDto>> AddUrl(UrlCreateDto urlCreateDto)
        {
            UrlDto createdUrl = await _urlService.CreateAsync(urlCreateDto);
            return CreatedAtAction(nameof(UrlsController.GetById), "Urls", new { id = createdUrl.Id }, createdUrl);
        }

        /// <summary>
        /// Modifies existing project
        /// </summary>
        /// <param name="projectModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(ProjectModifyDto projectModifyDto)
        {
            await _projectService.ModifyAsync(projectModifyDto);

            return NoContent();
        }

        /// <summary>
        /// Deletes existing project by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _projectService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Gets all participations for a given project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The project participations</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}/participations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectParticipationDto>>> GetParticipationsById([FromRoute] Guid id)
        {
            return Ok(await _projectService.GetParticipationsByIdAsync(id));
        }

        /// <summary>
        /// Gets all appointments for a given project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The appointments</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}/appointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AppointmentListDto>>> GetAppointmentsById([FromRoute] Guid id)
        {
            return Ok(await _projectService.GetAppointmentsByIdAsync(id));
        }

        /// <summary>
        /// Gets all appointments with participations for a given project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The appointments with participations</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}/appointments/full")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsWithParticipationsById([FromRoute] Guid id)
        {
            return Ok(await _projectService.GetAppointmentsWithParticipationsByIdAsync(id));
        }

        /// <summary>
        /// Sets the project participation for the given musician profile
        /// </summary>
        /// <param name="myProjectParticipationDto"></param>
        /// <returns>Project participation</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/participations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<ProjectParticipationDto>> SetProjectParticipation(SetProjectParticipationDto myProjectParticipationDto)
        {
            return Ok(await _projectService.SetProjectParticipationAsync(myProjectParticipationDto));
        }
    }
}
