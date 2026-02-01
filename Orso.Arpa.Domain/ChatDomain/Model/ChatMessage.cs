using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.ChatDomain.Model
{
    public class ChatMessage : BaseEntity
    {
        public ChatMessage(Guid? id, Guid chatRoomId, Guid senderId, string content, Guid? replyToMessageId = null) : base(id)
        {
            ChatRoomId = chatRoomId;
            SenderId = senderId;
            Content = content;
            ReplyToMessageId = replyToMessageId;
            SentAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        [JsonConstructor]
        protected ChatMessage()
        {
        }

        public void UpdateContent(string content)
        {
            Content = content;
            EditedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            // Keep the content for admin purposes but it won't be shown to users
        }

        /// <summary>
        /// Reference to the chat room
        /// </summary>
        public Guid ChatRoomId { get; private set; }
        public virtual ChatRoom ChatRoom { get; private set; }

        /// <summary>
        /// Reference to the sender user
        /// </summary>
        public Guid SenderId { get; private set; }
        public virtual User Sender { get; private set; }

        /// <summary>
        /// Message content (max 4000 characters)
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// When the message was sent
        /// </summary>
        public DateTime SentAt { get; private set; }

        /// <summary>
        /// When the message was last edited (null if never edited)
        /// </summary>
        public DateTime? EditedAt { get; private set; }

        /// <summary>
        /// Soft delete flag - deleted messages show "Message deleted" instead of content
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Optional: Reply to another message (threading)
        /// </summary>
        public Guid? ReplyToMessageId { get; private set; }
        public virtual ChatMessage ReplyToMessage { get; private set; }

        // Navigation properties
        [CascadingSoftDelete]
        public virtual ICollection<ChatMessageAttachment> Attachments { get; private set; } = new HashSet<ChatMessageAttachment>();

        [CascadingSoftDelete]
        public virtual ICollection<MessageReaction> Reactions { get; private set; } = new HashSet<MessageReaction>();

        [CascadingSoftDelete]
        public virtual ICollection<MessageReadReceipt> ReadReceipts { get; private set; } = new HashSet<MessageReadReceipt>();
    }
}
