using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orso.Arpa.Mail.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage emailMessage);

        Task SendTemplatedEmailAsync(ITemplate templateData, string receipientMail, IList<EmailAttachment> attachments = null);
        Task SendTemplatedEmailAsync(ITemplate templateData, IEnumerable<string> recipientMailList, IList<EmailAttachment> attachments = null);
    }
}
