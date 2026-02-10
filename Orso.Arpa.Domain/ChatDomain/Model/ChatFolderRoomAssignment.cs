using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.ChatDomain.Model
{
    public class ChatFolderRoomAssignment : BaseEntity
    {
        public ChatFolderRoomAssignment(Guid? id, Guid folderId, Guid chatRoomId, Guid? userId = null, int sortOrder = 0) : base(id)
        {
            FolderId = folderId;
            ChatRoomId = chatRoomId;
            UserId = userId;
            SortOrder = sortOrder;
        }

        [JsonConstructor]
        protected ChatFolderRoomAssignment()
        {
        }

        public void UpdateSortOrder(int sortOrder)
        {
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Reference to the folder
        /// </summary>
        public Guid FolderId { get; private set; }
        public virtual ChatFolder Folder { get; private set; }

        /// <summary>
        /// Reference to the chat room
        /// </summary>
        public Guid ChatRoomId { get; private set; }
        public virtual ChatRoom ChatRoom { get; private set; }

        /// <summary>
        /// NULL = system assignment (visible to all users).
        /// Set = personal assignment (only for this user).
        /// </summary>
        public Guid? UserId { get; private set; }
        public virtual User User { get; private set; }

        /// <summary>
        /// Sort order within the folder
        /// </summary>
        public int SortOrder { get; private set; }
    }
}
