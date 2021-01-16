using System.Threading.Tasks;

namespace Orso.Arpa.Mail
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessage emailMessage);

        Task SendEmailAsync(EmailMessage emailMessage);
    }
}
