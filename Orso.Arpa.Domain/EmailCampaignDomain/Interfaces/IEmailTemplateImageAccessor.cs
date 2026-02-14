using System.Threading.Tasks;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Interfaces
{
    public interface IEmailTemplateImageAccessor
    {
        Task<string> SaveAsync(string fileName, string contentType, byte[] data);

        Task<EmailTemplateImageResult> GetAsync(string storagePath);
    }

    public class EmailTemplateImageResult
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
