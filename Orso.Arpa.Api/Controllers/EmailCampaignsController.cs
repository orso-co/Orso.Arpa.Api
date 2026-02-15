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

public class EmailCampaignsController : BaseController
{
    private readonly IEmailCampaignService _emailCampaignService;

    public EmailCampaignsController(IEmailCampaignService emailCampaignService)
    {
        _emailCampaignService = emailCampaignService;
    }

    /// <summary>
    /// Gets all email campaigns
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EmailCampaignListDto>>> Get()
    {
        return Ok(await _emailCampaignService.GetAsync());
    }

    /// <summary>
    /// Gets an email campaign by id with recipients and attachments
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmailCampaignDto>> GetById([FromRoute] Guid id)
    {
        return Ok(await _emailCampaignService.GetByIdAsync(id));
    }

    /// <summary>
    /// Creates a new email campaign (draft)
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<EmailCampaignDto>> Post([FromBody] EmailCampaignCreateDto createDto)
    {
        EmailCampaignDto createdDto = await _emailCampaignService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
    }

    /// <summary>
    /// Modifies an existing email campaign
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Put(EmailCampaignModifyDto modifyDto)
    {
        await _emailCampaignService.ModifyAsync(modifyDto);
        return NoContent();
    }

    /// <summary>
    /// Adds recipients to a campaign from person IDs
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("{id:guid}/recipients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<AddRecipientsResult>> AddRecipients([FromRoute] Guid id, [FromBody] AddRecipientsBody body)
    {
        int addedCount = await _emailCampaignService.AddRecipientsAsync(id, body.PersonIds);
        return Ok(new AddRecipientsResult(addedCount));
    }

    /// <summary>
    /// Removes recipients from a campaign
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("{id:guid}/recipients/remove")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<RemoveRecipientsResult>> RemoveRecipients([FromRoute] Guid id, [FromBody] RemoveRecipientsBody body)
    {
        int removedCount = await _emailCampaignService.RemoveRecipientsAsync(id, body.PersonIds);
        return Ok(new RemoveRecipientsResult(removedCount));
    }

    /// <summary>
    /// Sends a campaign immediately
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("{id:guid}/send")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Send([FromRoute] Guid id)
    {
        await _emailCampaignService.SendAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Schedules a campaign for later sending
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("{id:guid}/schedule")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Schedule([FromRoute] Guid id, [FromBody] ScheduleBody body)
    {
        await _emailCampaignService.ScheduleAsync(id, body.ScheduledAt);
        return NoContent();
    }

    /// <summary>
    /// Cancels a campaign
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Cancel([FromRoute] Guid id)
    {
        await _emailCampaignService.CancelAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Gets campaign analytics
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet("{id:guid}/analytics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampaignAnalyticsDto>> GetAnalytics([FromRoute] Guid id)
    {
        return Ok(await _emailCampaignService.GetAnalyticsAsync(id));
    }

    /// <summary>
    /// Sends a test email for a campaign to the specified address
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("{id:guid}/send-test")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> SendTest([FromRoute] Guid id, [FromBody] SendTestEmailDto dto)
    {
        await _emailCampaignService.SendTestAsync(id, dto.EmailAddress);
        return NoContent();
    }

    /// <summary>
    /// Deletes an email campaign
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _emailCampaignService.DeleteAsync(id);
        return NoContent();
    }

    public record AddRecipientsBody(IList<Guid> PersonIds);
    public record AddRecipientsResult(int AddedCount);
    public record RemoveRecipientsBody(IList<Guid> PersonIds);
    public record RemoveRecipientsResult(int RemovedCount);
    public record ScheduleBody(DateTime ScheduledAt);
}
