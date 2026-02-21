using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.TicketDomain.Model
{
    public class TicketReaction : BaseEntity
    {
        public TicketReaction(Guid? id, Guid messageId, Guid userId, string emoji) : base(id)
        {
            MessageId = messageId;
            UserId = userId;
            Emoji = emoji;
        }

        [JsonConstructor]
        protected TicketReaction()
        {
        }

        public Guid MessageId { get; private set; }
        public virtual TicketMessage Message { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public string Emoji { get; private set; }
    }
}
