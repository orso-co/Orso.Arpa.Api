using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.ChatDomain.Model
{
    public class MessageReadReceipt : BaseEntity
    {
        public MessageReadReceipt(Guid? id, Guid messageId, Guid userId) : base(id)
        {
            MessageId = messageId;
            UserId = userId;
            ReadAt = DateTime.UtcNow;
        }

        [JsonConstructor]
        protected MessageReadReceipt()
        {
        }

        /// <summary>
        /// Reference to the message
        /// </summary>
        public Guid MessageId { get; private set; }
        public virtual ChatMessage Message { get; private set; }

        /// <summary>
        /// Reference to the user who read the message
        /// </summary>
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        /// <summary>
        /// When the message was read
        /// </summary>
        public DateTime ReadAt { get; private set; }
    }
}
