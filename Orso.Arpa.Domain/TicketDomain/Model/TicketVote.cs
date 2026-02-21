using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.TicketDomain.Model
{
    public class TicketVote : BaseEntity
    {
        public TicketVote(Guid? id, Guid ticketId, Guid userId, int value) : base(id)
        {
            TicketId = ticketId;
            UserId = userId;
            Value = value;
        }

        [JsonConstructor]
        protected TicketVote()
        {
        }

        public void UpdateValue(int value)
        {
            Value = value;
        }

        public Guid TicketId { get; private set; }
        public virtual Ticket Ticket { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public int Value { get; private set; }
    }
}
