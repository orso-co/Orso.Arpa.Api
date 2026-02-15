using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.Extensions.Configuration;
using MimeKit;
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
        private readonly IMjmlCompilationService _mjmlService;
        private readonly EmailConfiguration _emailConfig;
        private readonly IConfiguration _configuration;

        public Handler(
            IArpaContext arpaContext,
            IMjmlCompilationService mjmlService,
            EmailConfiguration emailConfig,
            IConfiguration configuration)
        {
            _arpaContext = arpaContext;
            _mjmlService = mjmlService;
            _emailConfig = emailConfig;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            EmailCampaign campaign = await _arpaContext.FindAsync<EmailCampaign>(new object[] { request.CampaignId }, cancellationToken)
                ?? throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(request.CampaignId), "Campaign not found.") { ErrorCode = "404" }
                });

            // Resolve HTML: PersonalizedHtml > MJML compilation > legacy CompiledHtml
            string html = campaign.PersonalizedHtml;
            if (string.IsNullOrWhiteSpace(html) && !string.IsNullOrWhiteSpace(campaign.EmailTemplate?.MjmlSource))
            {
                html = _mjmlService.CompileToHtml(campaign.EmailTemplate.MjmlSource);
            }
            html ??= campaign.EmailTemplate?.CompiledHtml;

            if (string.IsNullOrWhiteSpace(html))
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(campaign.EmailTemplateId), "Template has no HTML content to send.") { ErrorCode = "422" }
                });
            }

            // Inject dummy tracking (test token)
            var testToken = Guid.NewGuid();
            string baseUrl = _configuration.GetValue<string>("EmailCampaignConfiguration:BaseUrl") ?? "https://arpa.loopus.it";
            html = InjectTracking(html, testToken, baseUrl);

            // Send test email
            await SendEmailAsync(campaign.Subject, html, request.EmailAddress, baseUrl, testToken);

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

        private async Task SendEmailAsync(string subject, string htmlBody, string recipientEmail, string baseUrl, Guid trackingToken)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
            mimeMessage.To.Add(new MailboxAddress(recipientEmail, recipientEmail));
            mimeMessage.Subject = $"[TEST] {subject}";

            // RFC 8058 List-Unsubscribe headers
            mimeMessage.Headers.Add("List-Unsubscribe", $"<{baseUrl}/api/email/unsubscribe/{trackingToken}>");
            mimeMessage.Headers.Add("List-Unsubscribe-Post", "List-Unsubscribe=One-Click");

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, _emailConfig.Port == 465);
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            if (!string.IsNullOrWhiteSpace(_emailConfig.UserName) && !string.IsNullOrWhiteSpace(_emailConfig.Password))
            {
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
            }

            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}
