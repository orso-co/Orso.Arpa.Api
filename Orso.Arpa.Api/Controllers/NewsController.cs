using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.NewsApplication.Interfaces;
using Orso.Arpa.Application.NewsApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers;

public class NewsController : BaseController
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [Authorize(Roles = RoleNames.PerformerOrStaff)]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NewsDto>> GetById([FromRoute] Guid id)
    {
        return await _newsService.GetByIdAsync(id);
    }

    /// <summary>
    ///     Gets all news
    /// </summary>
    /// <returns>All news</returns>
    /// <response code="200"></response>
    [Authorize(Roles = RoleNames.PerformerOrStaff)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<NewsDto>>> Get(
        [FromQuery] int? limit,
        [FromQuery] int? offset,
        [FromQuery] bool includeHidden)
    {
        return Ok(await _newsService.GetAsync(limit, offset, includeHidden));
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<NewsDto>> Post([FromBody] NewsCreateDto createDto)
    {
        NewsDto createdDto = await _newsService.CreateAsync(createDto);

        return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Put(NewsModifyDto modifyDto)
    {
        await _newsService.ModifyAsync(modifyDto);

        return NoContent();
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _newsService.DeleteAsync(id);

        return NoContent();
    }

    [Authorize(Roles = RoleNames.PerformerOrStaff)]
    [HttpPut("{id:guid}/read")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkAsRead([FromRoute] Guid id)
    {
        await _newsService.MarkAsReadAsync(id);
        return NoContent();
    }

    [Authorize(Roles = RoleNames.PerformerOrStaff)]
    [HttpDelete("{id:guid}/read")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkAsUnread([FromRoute] Guid id)
    {
        await _newsService.MarkAsUnreadAsync(id);
        return NoContent();
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("{id:guid}/image")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetImage([FromRoute] Guid id, [FromForm] IFormFile file)
    {
        IFileResult fileResult = await _newsService.SetImageAsync(id, file);
        return CreatedAtAction(nameof(GetImage), new { id },
            File(fileResult.Content, fileResult.ContentType, $"News_Image_{id}{fileResult.Extension}"));
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}/image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImage([FromRoute] Guid id)
    {
        IFileResult fileResult = await _newsService.GetImageAsync(id);
        if (fileResult is null)
        {
            return NoContent();
        }
        return File(fileResult.Content, fileResult.ContentType, $"News_Image_{id}{fileResult.Extension}");
    }

    [Authorize(Roles = RoleNames.Staff)]
    [HttpDelete("{id:guid}/image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteImage([FromRoute] Guid id)
    {
        await _newsService.DeleteImageAsync(id);
        return NoContent();
    }
}
