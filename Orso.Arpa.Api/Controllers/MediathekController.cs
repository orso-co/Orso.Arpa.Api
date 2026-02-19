using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Orso.Arpa.Application.MediathekApplication.Interfaces;
using Orso.Arpa.Application.MediathekApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers;

[Route("api/mediathek")]
public class MediathekController : BaseController
{
    private readonly IMediathekService _mediathekService;
    private readonly IConfiguration _configuration;

    public MediathekController(IMediathekService mediathekService, IConfiguration configuration)
    {
        _mediathekService = mediathekService;
        _configuration = configuration;
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet("access")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MediathekAccessDto>>> GetAllAccesses()
    {
        return Ok(await _mediathekService.GetAllAccessesAsync());
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("access")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<MediathekAccessDto>> GrantAccess([FromBody] GrantMediathekAccessDto dto)
    {
        MediathekAccessDto result = await _mediathekService.GrantAccessAsync(dto);
        return Created(string.Empty, result);
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpDelete("access/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RevokeAccess([FromRoute] Guid id)
    {
        await _mediathekService.RevokeAccessAsync(id);
        return NoContent();
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet("requests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MediathekAccessRequestDto>>> GetPendingRequests()
    {
        return Ok(await _mediathekService.GetPendingRequestsAsync());
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("requests/{id:guid}/approve")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MediathekAccessDto>> ApproveRequest([FromRoute] Guid id)
    {
        MediathekAccessDto result = await _mediathekService.ApproveRequestAsync(id);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("requests/{id:guid}/deny")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DenyRequest([FromRoute] Guid id)
    {
        await _mediathekService.DenyRequestAsync(id);
        return NoContent();
    }

    [Authorize]
    [HttpGet("my-access")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MediathekMyAccessDto>> GetMyAccess()
    {
        return Ok(await _mediathekService.GetMyAccessAsync());
    }

    [Authorize]
    [HttpPost("request")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<MediathekAccessRequestDto>> RequestAccess([FromBody] RequestMediathekAccessDto dto)
    {
        MediathekAccessRequestDto result = await _mediathekService.RequestAccessAsync(dto);
        return Created(string.Empty, result);
    }

    [AllowAnonymous]
    [HttpGet("check/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<bool>> CheckAccess([FromRoute] string username)
    {
        string bridgeKey = Request.Headers["X-Bridge-Key"].ToString();
        string expectedKey = _configuration["MediathekBridgeKey"];

        if (string.IsNullOrEmpty(expectedKey) || bridgeKey != expectedKey)
        {
            return Unauthorized();
        }

        bool hasAccess = await _mediathekService.CheckAccessByUsernameAsync(username);
        return Ok(hasAccess);
    }
}
