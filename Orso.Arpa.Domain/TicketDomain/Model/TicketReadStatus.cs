using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.TicketDomain.Model
{
    public class TicketReadStatus : BaseEntity
    {
        public TicketReadStatus(Guid? id, Guid ticketId, Guid userId) : base(id)
        {
            TicketId = ticketId;
            UserId = userId;
            LastReadAt = DateTime.UtcNow;
        }

        [JsonConstructor]
        protected TicketReadStatus()
        {
        }

        public void MarkAsRead()
        {
            LastReadAt = DateTime.UtcNow;
        }

        public Guid TicketId { get; private set; }
        public virtual Ticket Ticket { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public DateTime LastReadAt { get; private set; }
    }
}
