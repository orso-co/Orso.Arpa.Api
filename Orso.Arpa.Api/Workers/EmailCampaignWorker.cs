using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using Orso.Arpa.Domain.EmailCampaignDomain.Enums;
using Orso.Arpa.Domain.EmailCampaignDomain.Interfaces;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Services;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Mail;

namespace Orso.Arpa.Api.Workers;

public sealed class EmailCampaignWorker : BackgroundService
{
    private readonly ILogger<EmailCampaignWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private const string LoggerPrefix = "EMAIL_CAMPAIGN_WORKER:";
    private const int BatchSize = 14;
    private const int BatchDelayMs = 1000;
    private const int PollIntervalMs = 30000;

    public EmailCampaignWorker(
        ILogger<EmailCampaignWorker> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("{Prefix} Worker started", LoggerPrefix);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using IServiceScope scope = _serviceProvider.CreateScope();
                IArpaContext arpaContext = scope.ServiceProvider.GetRequiredService<IArpaContext>();

                // 1. Check for scheduled campaigns ready to send
                await ActivateScheduledCampaigns(arpaContext, stoppingToken);

                // 2. Process sending campaigns
                await ProcessSendingCampaigns(arpaContext, scope.ServiceProvider, stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Prefix} An error occurred while executing", LoggerPrefix);
            }

            await Task.Delay(PollIntervalMs, stoppingToken);
        }
    }

    private async Task ActivateScheduledCampaigns(IArpaContext arpaContext, CancellationToken cancellationToken)
    {
        List<EmailCampaign> scheduledCampaigns = await arpaContext.Set<EmailCampaign>()
            .Where(c => c.Status == CampaignStatus.Scheduled
                && c.ScheduledAt <= DateTime.UtcNow
                && !c.Deleted)
            .ToListAsync(cancellationToken);

        foreach (EmailCampaign campaign in scheduledCampaigns)
        {
            _logger.LogInformation("{Prefix} Activating scheduled campaign '{Name}' (ID: {Id})",
                LoggerPrefix, campaign.Name, campaign.Id);
            campaign.StartSending();
        }

        if (scheduledCampaigns.Count > 0)
        {
            await arpaContext.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task ProcessSendingCampaigns(IArpaContext arpaContext, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        List<EmailCampaign> sendingCampaigns = await arpaContext.Set<EmailCampaign>()
            .Where(c => c.Status == CampaignStatus.Sending && !c.Deleted)
            .ToListAsync(cancellationToken);

        foreach (EmailCampaign campaign in sendingCampaigns)
        {
            await ProcessCampaignBatch(arpaContext, serviceProvider, campaign, cancellationToken);
        }
    }

    private async Task ProcessCampaignBatch(IArpaContext arpaContext, IServiceProvider serviceProvider, EmailCampaign campaign, CancellationToken cancellationToken)
    {
        // Get next batch of pending recipients
        List<EmailCampaignRecipient> pendingRecipients = await arpaContext.Set<EmailCampaignRecipient>()
            .Where(r => r.EmailCampaignId == campaign.Id
                && r.Status == RecipientStatus.Pending
                && !r.Deleted)
            .Take(BatchSize)
            .ToListAsync(cancellationToken);

        if (pendingRecipients.Count == 0)
        {
            // All recipients processed
            campaign.MarkAsSent();
            await arpaContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Prefix} Campaign '{Name}' completed. Sent: {Sent}, Failed: {Failed}",
                LoggerPrefix, campaign.Name, campaign.SentCount, campaign.FailedCount);
            return;
        }

        // Use a projection query to load only the HTML fields we need.
        // This avoids lazy-loading the full EmailTemplate entity which can be 85MB+
        // (68MB ProjectDataJson + 8.5MB MjmlSource + 8.5MB CompiledHtml).
        var htmlData = await arpaContext.Set<EmailCampaign>()
            .Where(c => c.Id == campaign.Id)
            .Select(c => new
            {
                c.PersonalizedHtml,
                TemplateCompiledHtml = c.EmailTemplate != null ? c.EmailTemplate.CompiledHtml : null,
                TemplateMjmlSource = c.EmailTemplate != null ? c.EmailTemplate.MjmlSource : null,
            })
            .FirstOrDefaultAsync(cancellationToken);

        string html = htmlData?.PersonalizedHtml;
        html ??= htmlData?.TemplateCompiledHtml;
        if (string.IsNullOrWhiteSpace(html) && !string.IsNullOrWhiteSpace(htmlData?.TemplateMjmlSource))
        {
            var mjmlService = serviceProvider.GetRequiredService<IMjmlCompilationService>();
            html = mjmlService.CompileToHtml(htmlData.TemplateMjmlSource);
        }
        if (string.IsNullOrWhiteSpace(html))
        {
            _logger.LogError("{Prefix} Campaign '{Name}' has no HTML content", LoggerPrefix, campaign.Name);
            campaign.MarkAsFailed();
            await arpaContext.SaveChangesAsync(cancellationToken);
            return;
        }

        EmailConfiguration emailConfig = GetCampaignEmailConfig(serviceProvider);
        string baseUrl = GetBaseUrl(serviceProvider);

        // Replace base64 data URIs with hosted image URLs to reduce email size
        var imageAccessor = serviceProvider.GetRequiredService<IEmailTemplateImageAccessor>();
        html = await EmailHtmlImageInliner.ReplaceBase64WithUrlsAsync(html, baseUrl, imageAccessor);

        foreach (EmailCampaignRecipient recipient in pendingRecipients)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            try
            {
                string personalizedHtml = InjectTracking(html, recipient.TrackingToken, baseUrl);
                await SendEmailAsync(emailConfig, campaign.Subject, personalizedHtml, recipient.EmailAddress, recipient.DisplayName, recipient.TrackingToken, baseUrl);
                recipient.MarkAsSent();
                campaign.IncrementSentCount();
                _logger.LogDebug("{Prefix} Sent to {Email}", LoggerPrefix, recipient.EmailAddress);
            }
            catch (Exception e)
            {
                recipient.MarkAsFailed(e.Message);
                campaign.IncrementFailedCount();
                _logger.LogWarning(e, "{Prefix} Failed to send to {Email}", LoggerPrefix, recipient.EmailAddress);
            }
        }

        await arpaContext.SaveChangesAsync(cancellationToken);

        // Rate limiting: wait between batches
        await Task.Delay(BatchDelayMs, cancellationToken);
    }

    private static string InjectTracking(string html, Guid trackingToken, string baseUrl)
    {
        string trackingPixel = $"<img src=\"{baseUrl}/api/email/track/{trackingToken}.png\" width=\"1\" height=\"1\" style=\"display:none\" alt=\"\" />";
        string unsubscribeLink = $"<p style=\"text-align:center;font-size:12px;color:#999;margin-top:20px;\"><a href=\"{baseUrl}/api/email/unsubscribe/{trackingToken}\" style=\"color:#999;\">Newsletter abbestellen</a></p>";

        if (html.Contains("</body>", StringComparison.OrdinalIgnoreCase))
        {
            html = html.Replace("</body>", $"{trackingPixel}{unsubscribeLink}</body>", StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            html += trackingPixel + unsubscribeLink;
        }

        return html;
    }

    private static async Task SendEmailAsync(EmailConfiguration emailConfig, string subject, string htmlBody, string recipientEmail, string recipientName, Guid trackingToken, string baseUrl)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(emailConfig.From, emailConfig.From));
        mimeMessage.To.Add(new MailboxAddress(recipientName, recipientEmail));
        mimeMessage.Subject = subject;

        // RFC 8058 List-Unsubscribe headers
        mimeMessage.Headers.Add("List-Unsubscribe", $"<{baseUrl}/api/email/unsubscribe/{trackingToken}>");
        mimeMessage.Headers.Add("List-Unsubscribe-Post", "List-Unsubscribe=One-Click");

        var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(emailConfig.SmtpServer, emailConfig.Port, emailConfig.Port == 465);
        client.AuthenticationMechanisms.Remove("XOAUTH2");

        if (!string.IsNullOrWhiteSpace(emailConfig.UserName) && !string.IsNullOrWhiteSpace(emailConfig.Password))
        {
            await client.AuthenticateAsync(emailConfig.UserName, emailConfig.Password);
        }

        await client.SendAsync(mimeMessage);
        await client.DisconnectAsync(true);
    }

    private static EmailConfiguration GetCampaignEmailConfig(IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var campaignSection = configuration.GetSection("EmailCampaignConfiguration");

        // Use dedicated campaign SMTP settings if configured, otherwise fall back to default EmailConfiguration
        string smtpServer = campaignSection.GetValue<string>("SmtpServer");
        if (!string.IsNullOrWhiteSpace(smtpServer))
        {
            return new EmailConfiguration
            {
                SmtpServer = smtpServer,
                Port = campaignSection.GetValue("Port", 587),
                UserName = campaignSection.GetValue<string>("Username"),
                Password = campaignSection.GetValue<string>("Password"),
                From = campaignSection.GetValue<string>("From") ?? serviceProvider.GetRequiredService<EmailConfiguration>().From,
            };
        }

        return serviceProvider.GetRequiredService<EmailConfiguration>();
    }

    private static string GetBaseUrl(IServiceProvider serviceProvider)
    {
        // Use configuration to determine the base URL for tracking links
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetValue<string>("EmailCampaignConfiguration:BaseUrl") ?? "https://arpa.loopus.it";
    }
}
