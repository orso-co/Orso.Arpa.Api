using System.Threading.Tasks;

namespace Orso.Arpa.Mail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}
