using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.EmailCampaignDomain.Commands;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Api.Controllers;

[ApiController]
[Route("api/email")]
[AllowAnonymous]
public class EmailTrackingController : ControllerBase
{
    private readonly IArpaContext _arpaContext;
    private readonly IMediator _mediator;

    // 1x1 transparent PNG pixel
    private static readonly byte[] TransparentPixel =
    [
        0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x00, 0x00, 0x0D,
        0x49, 0x48, 0x44, 0x52, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01,
        0x08, 0x06, 0x00, 0x00, 0x00, 0x1F, 0x15, 0xC4, 0x89, 0x00, 0x00, 0x00,
        0x0A, 0x49, 0x44, 0x41, 0x54, 0x78, 0x9C, 0x62, 0x00, 0x00, 0x00, 0x02,
        0x00, 0x01, 0xE5, 0x27, 0xDE, 0xFC, 0x00, 0x00, 0x00, 0x00, 0x49, 0x45,
        0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82
    ];

    public EmailTrackingController(IArpaContext arpaContext, IMediator mediator)
    {
        _arpaContext = arpaContext;
        _mediator = mediator;
    }

    /// <summary>
    /// Tracking pixel endpoint - records email opens
    /// </summary>
    [HttpGet("track/{trackingToken}.png")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> TrackOpen([FromRoute] Guid trackingToken)
    {
        try
        {
            EmailCampaignRecipient recipient = await _arpaContext.Set<EmailCampaignRecipient>()
                .FirstOrDefaultAsync(r => r.TrackingToken == trackingToken && !r.Deleted);

            if (recipient != null && recipient.OpenedAt == null)
            {
                recipient.MarkAsOpened();

                EmailCampaign campaign = await _arpaContext.FindAsync<EmailCampaign>(new object[] { recipient.EmailCampaignId });
                campaign?.IncrementOpenedCount();

                await _arpaContext.SaveChangesAsync(default);
            }
        }
        catch
        {
            // Tracking should never fail the response
        }

        return File(TransparentPixel, "image/png");
    }

    /// <summary>
    /// Unsubscribe page - allows recipients to opt out
    /// </summary>
    [HttpGet("unsubscribe/{trackingToken}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Unsubscribe([FromRoute] Guid trackingToken)
    {
        try
        {
            await _mediator.Send(new Unsubscribe.Command { TrackingToken = trackingToken });
        }
        catch
        {
            // Show success page even on error to not leak information
        }

        return Content(GetUnsubscribeHtml(), "text/html");
    }

    /// <summary>
    /// One-click unsubscribe (RFC 8058)
    /// </summary>
    [HttpPost("unsubscribe/{trackingToken}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UnsubscribeOneClick([FromRoute] Guid trackingToken)
    {
        try
        {
            await _mediator.Send(new Unsubscribe.Command { TrackingToken = trackingToken });
        }
        catch
        {
            // Always return success for one-click unsubscribe
        }

        return Ok();
    }

    private static string GetUnsubscribeHtml()
    {
        return """
            <!DOCTYPE html>
            <html lang="de">
            <head>
                <meta charset="UTF-8">
                <meta name="viewport" content="width=device-width, initial-scale=1.0">
                <title>Abmeldung erfolgreich</title>
                <style>
                    body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif; display: flex; justify-content: center; align-items: center; min-height: 100vh; margin: 0; background: #f5f5f5; }
                    .card { background: white; padding: 2rem; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.1); max-width: 400px; text-align: center; }
                    h1 { color: #333; font-size: 1.5rem; }
                    p { color: #666; line-height: 1.6; }
                </style>
            </head>
            <body>
                <div class="card">
                    <h1>Abmeldung erfolgreich</h1>
                    <p>Sie wurden erfolgreich von unserem Newsletter abgemeldet und erhalten keine weiteren E-Mails mehr.</p>
                </div>
            </body>
            </html>
            """;
    }
}
