using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace Orso.Arpa.Mail
{
    public class EmailMessage
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IList<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();

        public EmailMessage(IEnumerable<string> to,
                            string subject,
                            string content,
                            IList<EmailAttachment> emailAttachments)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x, x)));
            Subject = subject;
            Content = content;
            if (emailAttachments != null)
            {
                Attachments = emailAttachments;
            }
        }
    }

    public class EmailAttachment
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
    }
}
