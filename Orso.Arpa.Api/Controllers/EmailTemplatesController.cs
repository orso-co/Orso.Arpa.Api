using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.EmailCampaignApplication.Interfaces;
using Orso.Arpa.Application.EmailCampaignApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers;

public class EmailTemplatesController : BaseController
{
    private readonly IEmailTemplateService _emailTemplateService;

    public EmailTemplatesController(IEmailTemplateService emailTemplateService)
    {
        _emailTemplateService = emailTemplateService;
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
}
