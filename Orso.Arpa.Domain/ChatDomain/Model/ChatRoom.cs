using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.ChatDomain.Enums;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.ChatDomain.Model
{
    public class ChatRoom : BaseEntity
    {
        public ChatRoom(Guid? id, ChatRoomType type, string name = null, Guid? projectId = null) : base(id)
        {
            Type = type;
            Name = name;
            ProjectId = projectId;
        }

        [JsonConstructor]
        protected ChatRoom()
        {
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateLastMessageAt(DateTime lastMessageAt)
        {
            LastMessageAt = lastMessageAt;
        }

        /// <summary>
        /// Type of chat room (Direct, Project, Global)
        /// </summary>
        public ChatRoomType Type { get; private set; }

        /// <summary>
        /// Optional name for project/global chats. Null for direct chats.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// For project chats - links to the project
        /// </summary>
        public Guid? ProjectId { get; private set; }
        public virtual Project Project { get; private set; }

        /// <summary>
        /// Timestamp of the last message for sorting
        /// </summary>
        public DateTime? LastMessageAt { get; private set; }

        // Navigation properties
        [CascadingSoftDelete]
        public virtual ICollection<ChatRoomMember> Members { get; private set; } = new HashSet<ChatRoomMember>();

        [CascadingSoftDelete]
        public virtual ICollection<ChatMessage> Messages { get; private set; } = new HashSet<ChatMessage>();
    }
}
