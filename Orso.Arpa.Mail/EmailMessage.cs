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
        public bool UseHtml { get; set; }

        public EmailMessage(IEnumerable<string> to, string subject, string content, bool useHtml)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            UseHtml = useHtml;
        }
    }
}
