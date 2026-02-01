using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.ChatDomain.Model
{
    public class ChatRoomMember : BaseEntity
    {
        public ChatRoomMember(Guid? id, Guid chatRoomId, Guid userId) : base(id)
        {
            ChatRoomId = chatRoomId;
            UserId = userId;
            JoinedAt = DateTime.UtcNow;
            IsMuted = false;
        }

        [JsonConstructor]
        protected ChatRoomMember()
        {
        }

        public void UpdateLastReadAt(DateTime lastReadAt)
        {
            LastReadAt = lastReadAt;
        }

        public void SetMuted(bool isMuted)
        {
            IsMuted = isMuted;
        }

        public void SetHistoryVisibleFrom(DateTime? historyVisibleFrom)
        {
            HistoryVisibleFrom = historyVisibleFrom;
        }

        /// <summary>
        /// Reference to the chat room
        /// </summary>
        public Guid ChatRoomId { get; private set; }
        public virtual ChatRoom ChatRoom { get; private set; }

        /// <summary>
        /// Reference to the user
        /// </summary>
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        /// <summary>
        /// When the user joined the chat room
        /// </summary>
        public DateTime JoinedAt { get; private set; }

        /// <summary>
        /// When the user last read messages in this room (for unread count)
        /// </summary>
        public DateTime? LastReadAt { get; private set; }

        /// <summary>
        /// If set, user can only see messages from this date onward.
        /// Null means user can see all history.
        /// </summary>
        public DateTime? HistoryVisibleFrom { get; private set; }

        /// <summary>
        /// Whether notifications are muted for this chat
        /// </summary>
        public bool IsMuted { get; private set; }
    }
}
