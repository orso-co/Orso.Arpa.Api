using System;
using System.Threading.Tasks;

namespace Orso.Arpa.Domain.TicketDomain.Interfaces
{
    public interface ITicketFileAccessor
    {
        Task<string> SaveAsync(Guid ticketId, string fileName, string contentType, byte[] data);
        Task<TicketAttachmentFileResult> GetAsync(string storagePath);
        Task DeleteAsync(string storagePath);
    }

    public class TicketAttachmentFileResult
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
    }
}
