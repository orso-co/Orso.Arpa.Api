using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.Projects;
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

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> Get([FromQuery]bool includeCompleted = false)
        {
            return Ok(await _projectService.GetAsync(includeCompleted));
        }
    }
}
