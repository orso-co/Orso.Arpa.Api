using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.TicketDomain.Model
{
    public class TicketAttachment : BaseEntity
    {
        public TicketAttachment(Guid? id, string fileName, string contentType, string storagePath, long fileSize, Guid? ticketId = null, Guid? messageId = null) : base(id)
        {
            FileName = fileName;
            ContentType = contentType;
            StoragePath = storagePath;
            FileSize = fileSize;
            TicketId = ticketId;
            MessageId = messageId;
        }

        [JsonConstructor]
        protected TicketAttachment()
        {
        }

        public string FileName { get; private set; }
        public string ContentType { get; private set; }
        public string StoragePath { get; private set; }
        public long FileSize { get; private set; }

        public Guid? TicketId { get; private set; }
        public virtual Ticket Ticket { get; private set; }

        public Guid? MessageId { get; private set; }
        public virtual TicketMessage Message { get; private set; }

        public bool IsImage => ContentType?.StartsWith("image/") ?? false;
    }
}
