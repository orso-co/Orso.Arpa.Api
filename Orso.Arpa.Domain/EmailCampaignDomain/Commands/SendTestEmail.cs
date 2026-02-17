using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Orso.Arpa.Domain.EmailCampaignDomain.Interfaces;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Services;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Mail;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Commands;

public static class SendTestEmail
{
    public class Command : IRequest
    {
        public Guid CampaignId { get; set; }
        public string EmailAddress { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator(IArpaContext arpaContext)
        {
            _ = RuleFor(d => d.CampaignId)
                .EntityExists<Command, EmailCampaign>(arpaContext);

            _ = RuleFor(d => d.EmailAddress)
                .NotEmpty()
                .EmailAddress();
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IArpaContext _arpaContext;
        private readonly EmailConfiguration _defaultEmailConfig;
        private readonly IConfiguration _configuration;
        private readonly IEmailTemplateImageAccessor _imageAccessor;

        public Handler(
            IArpaContext arpaContext,
            EmailConfiguration emailConfig,
            IConfiguration configuration,
            IEmailTemplateImageAccessor imageAccessor)
        {
            _arpaContext = arpaContext;
            _defaultEmailConfig = emailConfig;
            _configuration = configuration;
            _imageAccessor = imageAccessor;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            // Use a projection query to only load the fields we need
            // This avoids loading the full template (ProjectDataJson can be 68+ MB)
            var campaignData = await _arpaContext.Set<EmailCampaign>()
                .Where(c => c.Id == request.CampaignId)
                .Select(c => new
                {
                    c.Subject,
                    c.PersonalizedHtml,
                    TemplateCompiledHtml = c.EmailTemplate != null ? c.EmailTemplate.CompiledHtml : null,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (campaignData == null)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(request.CampaignId), "Campaign not found.") { ErrorCode = "404" }
                });
            }

            // Resolve HTML: PersonalizedHtml > CompiledHtml
            string html = campaignData.PersonalizedHtml ?? campaignData.TemplateCompiledHtml;

            if (string.IsNullOrWhiteSpace(html))
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("EmailTemplateId", "Template has no HTML content to send.") { ErrorCode = "422" }
                });
            }

            // Replace base64 data URIs with hosted image URLs to reduce email size
            string baseUrl = _configuration.GetValue<string>("EmailCampaignConfiguration:BaseUrl") ?? "https://arpa.loopus.it";
            html = await EmailHtmlImageInliner.ReplaceBase64WithUrlsAsync(html, baseUrl, _imageAccessor);

            // Inject dummy tracking (test token)
            var testToken = Guid.NewGuid();
            html = InjectTracking(html, testToken, baseUrl);

            // Send test email using campaign SMTP config if available
            EmailConfiguration emailConfig = GetCampaignEmailConfig();
            await SendEmailAsync(emailConfig, campaignData.Subject, html, request.EmailAddress, baseUrl, testToken);

            return Unit.Value;
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

        private EmailConfiguration GetCampaignEmailConfig()
        {
            var campaignSection = _configuration.GetSection("EmailCampaignConfiguration");
            string smtpServer = campaignSection.GetValue<string>("SmtpServer");
            if (!string.IsNullOrWhiteSpace(smtpServer))
            {
                return new EmailConfiguration
                {
                    SmtpServer = smtpServer,
                    Port = campaignSection.GetValue("Port", 587),
                    UserName = campaignSection.GetValue<string>("Username"),
                    Password = campaignSection.GetValue<string>("Password"),
                    From = campaignSection.GetValue<string>("From") ?? _defaultEmailConfig.From,
                };
            }

            return _defaultEmailConfig;
        }

        private static async Task SendEmailAsync(EmailConfiguration emailConfig, string subject, string htmlBody, string recipientEmail, string baseUrl, Guid trackingToken)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(emailConfig.From, emailConfig.From));
            mimeMessage.To.Add(new MailboxAddress(recipientEmail, recipientEmail));
            mimeMessage.Subject = $"[TEST] {subject}";

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
    }
}
