using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.ChatDomain.Model
{
    public class ChatRoomEntityLink : BaseEntity
    {
        public ChatRoomEntityLink(Guid? id, Guid chatRoomId, string entityType, Guid entityId, string entityDisplayName = null) : base(id)
        {
            ChatRoomId = chatRoomId;
            EntityType = entityType;
            EntityId = entityId;
            EntityDisplayName = entityDisplayName;
        }

        [JsonConstructor]
        protected ChatRoomEntityLink()
        {
        }

        public Guid ChatRoomId { get; private set; }
        public virtual ChatRoom ChatRoom { get; private set; }

        public string EntityType { get; private set; }
        public Guid EntityId { get; private set; }
        public string EntityDisplayName { get; private set; }

        public void UpdateDisplayName(string displayName)
        {
            EntityDisplayName = displayName;
        }
    }
}
