using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Application.MyProjectApplication;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers;

[Route("api/me/projects")]
public class MyProjectsController : BaseController
{
    private readonly IMyProjectService _myProjectService;

    public MyProjectsController(IMyProjectService myProjectService)
    {
        _myProjectService = myProjectService;
    }

    /// <summary>
    /// Gets all my projects
    /// </summary>
    /// <returns>All my projects of the current user</returns>
    /// <response code="200"></response>
    [HttpGet]
    [Authorize(Roles = RoleNames.Performer)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MyProjectDto>>> Get()
    {
        return Ok(await _myProjectService.GetMyProjectsAsync());
    }
}
