using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Orso.Arpa.Mail.Interfaces;

namespace Orso.Arpa.Mail
{
    /// <summary>
    /// Sends Email via smtp in either a synchronous or async way
    /// </summary>
    /// <see cref="https://code-maze.com/send-email-with-attachments-aspnetcore-2/"/>
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ITemplateParser _templateParser;

        public EmailSender(EmailConfiguration emailConfig, ITemplateParser templateParser)
        {
            _emailConfig = emailConfig;
            _templateParser = templateParser;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            MimeMessage mimeMessage = CreateEmailMessage(emailMessage);
            await SendAsync(mimeMessage);
        }

        public async Task SendTemplatedEmailAsync(ITemplate templateData, string receipientMail)
        {
            var templatedBody = _templateParser.Parse(templateData);

            var subject = _emailConfig.DefaultSubject;
            if (templatedBody.Contains("<title>") && templatedBody.Contains("</title>"))
            {
                subject = templatedBody.Split("<title>")[1].Split("</title>")[0];
            }

            var mailMessage = new EmailMessage(new string[] { receipientMail }, subject, templatedBody, true);
            await SendEmailAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage emailMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailConfig.From));
            mimeMessage.To.AddRange(emailMessage.To);
            mimeMessage.Subject = emailMessage.Subject ?? _emailConfig.DefaultSubject;
            if (emailMessage.UseHtml)
            {
                mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailMessage.Content };
            }
            else
            {
                mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = emailMessage.Content };
            }
            return mimeMessage;
        }

        private async Task SendAsync(MimeMessage mimeMessage)
        {
            using var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, _emailConfig.Port == 465);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!string.IsNullOrWhiteSpace(_emailConfig.UserName) && !string.IsNullOrWhiteSpace(_emailConfig.Password))
                {
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                }

                await client.SendAsync(mimeMessage);
            }
            catch (Exception ex)
            {
                throw new EmailException("Error sending an email", ex);
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
