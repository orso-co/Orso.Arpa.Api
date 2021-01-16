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

        public void SendEmail(EmailMessage emailMessage)
        {
            MimeMessage mimeMessage = CreateEmailMessage(emailMessage);
            Send(mimeMessage);
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

        private void Send(MimeMessage mimeMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                client.Send(mimeMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        private async Task SendAsync(MimeMessage mimeMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                await client.SendAsync(mimeMessage);
            }
            catch
            {
                //log an error message or throw an exception, or both.
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
