using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.TicketDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.TicketDomain.Model
{
    public class Ticket : BaseEntity
    {
        public Ticket(Guid? id, string title, string description, TicketType type, Guid creatorId) : base(id)
        {
            Title = title;
            Description = description;
            Type = type;
            Status = TicketStatus.Open;
            CreatorId = creatorId;
        }

        [JsonConstructor]
        protected Ticket()
        {
        }

        public void Update(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public void UpdateAdminFields(TicketStatus? status, int? adminPriority, TicketEffort? effort, int? estimatedMinutes, int? spentMinutes, TicketType? type)
        {
            if (status.HasValue) Status = status.Value;
            if (adminPriority.HasValue) AdminPriority = adminPriority.Value;
            if (type.HasValue) Type = type.Value;
            Effort = effort;
            EstimatedMinutes = estimatedMinutes;
            SpentMinutes = spentMinutes;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public TicketType Type { get; private set; }
        public TicketStatus Status { get; private set; }

        public int? AdminPriority { get; private set; }
        public TicketEffort? Effort { get; private set; }
        public int? EstimatedMinutes { get; private set; }
        public int? SpentMinutes { get; private set; }

        public Guid CreatorId { get; private set; }
        public virtual User Creator { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<TicketMessage> Messages { get; private set; } = new HashSet<TicketMessage>();

        [CascadingSoftDelete]
        public virtual ICollection<TicketAttachment> Attachments { get; private set; } = new HashSet<TicketAttachment>();

        [CascadingSoftDelete]
        public virtual ICollection<TicketLink> Links { get; private set; } = new HashSet<TicketLink>();

        [CascadingSoftDelete]
        public virtual ICollection<TicketVote> Votes { get; private set; } = new HashSet<TicketVote>();

        [CascadingSoftDelete]
        public virtual ICollection<TicketReadStatus> ReadStatuses { get; private set; } = new HashSet<TicketReadStatus>();
    }
}
