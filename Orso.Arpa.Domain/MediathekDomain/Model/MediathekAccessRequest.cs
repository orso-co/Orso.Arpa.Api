using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MediathekDomain.Enums;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.MediathekDomain.Model
{
    public class MediathekAccessRequest : BaseEntity
    {
        public MediathekAccessRequest(Guid? id, Guid personId, string message) : base(id)
        {
            PersonId = personId;
            Status = MediathekAccessRequestStatus.Pending;
            RequestedAt = DateTime.UtcNow;
            Message = message;
        }

        public MediathekAccessRequest()
        {
        }

        public Guid PersonId { get; private set; }
        public MediathekAccessRequestStatus Status { get; private set; }
        public DateTime RequestedAt { get; private set; }
        public DateTime? ProcessedAt { get; private set; }
        public string ProcessedBy { get; private set; }
        public string Message { get; private set; }

        public virtual Person Person { get; private set; }

        public void Approve(string processedBy)
        {
            Status = MediathekAccessRequestStatus.Approved;
            ProcessedAt = DateTime.UtcNow;
            ProcessedBy = processedBy;
        }

        public void Deny(string processedBy)
        {
            Status = MediathekAccessRequestStatus.Denied;
            ProcessedAt = DateTime.UtcNow;
            ProcessedBy = processedBy;
        }
    }
}
