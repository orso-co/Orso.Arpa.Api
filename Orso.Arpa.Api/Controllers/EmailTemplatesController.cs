using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.EmailCampaignApplication.Interfaces;
using Orso.Arpa.Application.EmailCampaignApplication.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers;

public class EmailTemplatesController : BaseController
{
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IEmailTemplateImageAccessor _imageAccessor;

    public EmailTemplatesController(
        IEmailTemplateService emailTemplateService,
        IEmailTemplateImageAccessor imageAccessor)
    {
        _emailTemplateService = emailTemplateService;
        _imageAccessor = imageAccessor;
    }

    /// <summary>
    /// Gets all email templates
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EmailTemplateDto>>> Get()
    {
        return Ok(await _emailTemplateService.GetAsync());
    }

    /// <summary>
    /// Gets an email template by id
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmailTemplateDto>> GetById([FromRoute] Guid id)
    {
        return Ok(await _emailTemplateService.GetByIdAsync(id));
    }

    /// <summary>
    /// Creates a new email template
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost]
    [RequestSizeLimit(104_857_600)] // 100 MB for template JSON + HTML
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<EmailTemplateDto>> Post([FromBody] EmailTemplateCreateDto createDto)
    {
        EmailTemplateDto createdDto = await _emailTemplateService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
    }

    /// <summary>
    /// Modifies an existing email template
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("{id}")]
    [RequestSizeLimit(104_857_600)] // 100 MB for template JSON + HTML
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Put(EmailTemplateModifyDto modifyDto)
    {
        await _emailTemplateService.ModifyAsync(modifyDto);
        return NoContent();
    }

    /// <summary>
    /// Deletes an email template
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _emailTemplateService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Uploads images for email templates
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("images")]
    [RequestSizeLimit(10_485_760)] // 10 MB
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UploadImages([FromForm] List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
        {
            return BadRequest("No files uploaded");
        }

        var results = new List<object>();

        foreach (IFormFile file in files)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            byte[] data = ms.ToArray();

            string storagePath = await _imageAccessor.SaveAsync(
                file.FileName,
                file.ContentType,
                data);

            results.Add(new
            {
                src = $"/api/emailtemplates/images/{storagePath}",
                type = "image",
                name = file.FileName,
            });
        }

        return Ok(new { data = results });
    }

    /// <summary>
    /// Serves an uploaded email template image
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet("images/{filename}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImage([FromRoute] string filename)
    {
        EmailTemplateImageResult result = await _imageAccessor.GetAsync(filename);

        if (result == null)
        {
            return NotFound();
        }

        return File(result.Content, result.ContentType, result.FileName);
    }
}
