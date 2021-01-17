using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Orso.Arpa.Mail
{
    /// <summary>
    /// Sends Email via smtp in either a synchronous or async way
    /// </summary>
    /// <see cref="https://code-maze.com/send-email-with-attachments-aspnetcore-2/"/>
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            MimeMessage mimeMessage = CreateEmailMessage(emailMessage);
            await SendAsync(mimeMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage emailMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailConfig.From));
            mimeMessage.To.AddRange(emailMessage.To);
            mimeMessage.Subject = emailMessage.Subject;
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
