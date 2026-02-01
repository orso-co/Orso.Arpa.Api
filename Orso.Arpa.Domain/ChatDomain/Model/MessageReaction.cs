using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.ChatDomain.Model
{
    public class MessageReaction : BaseEntity
    {
        public MessageReaction(Guid? id, Guid messageId, Guid userId, string emoji) : base(id)
        {
            MessageId = messageId;
            UserId = userId;
            Emoji = emoji;
        }

        [JsonConstructor]
        protected MessageReaction()
        {
        }

        /// <summary>
        /// Reference to the message
        /// </summary>
        public Guid MessageId { get; private set; }
        public virtual ChatMessage Message { get; private set; }

        /// <summary>
        /// Reference to the user who reacted
        /// </summary>
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        /// <summary>
        /// The emoji used for the reaction (unicode emoji or shortcode)
        /// </summary>
        public string Emoji { get; private set; }
    }
}
