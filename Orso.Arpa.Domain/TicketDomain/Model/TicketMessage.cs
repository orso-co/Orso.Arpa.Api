using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.TicketDomain.Model
{
    public class TicketMessage : BaseEntity
    {
        public TicketMessage(Guid? id, Guid ticketId, Guid userId, string content) : base(id)
        {
            TicketId = ticketId;
            UserId = userId;
            Content = content;
            SentAt = DateTime.UtcNow;
        }

        [JsonConstructor]
        protected TicketMessage()
        {
        }

        public Guid TicketId { get; private set; }
        public virtual Ticket Ticket { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public string Content { get; private set; }
        public DateTime SentAt { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<TicketAttachment> Attachments { get; private set; } = new HashSet<TicketAttachment>();

        [CascadingSoftDelete]
        public virtual ICollection<TicketReaction> Reactions { get; private set; } = new HashSet<TicketReaction>();
    }
}
