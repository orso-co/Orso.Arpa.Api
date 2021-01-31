using System.Threading.Tasks;

namespace Orso.Arpa.Mail.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage emailMessage);

        Task SendTemplatedEmailAsync(ITemplate templateData, string receipientMail);
    }
}
