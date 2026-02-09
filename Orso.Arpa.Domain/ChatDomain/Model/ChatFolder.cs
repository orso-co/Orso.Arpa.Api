using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.ChatDomain.Model
{
    public class ChatFolder : BaseEntity
    {
        public ChatFolder(Guid? id, string name, bool isSystem, Guid? ownerId = null, Guid? parentId = null, int sortOrder = 0) : base(id)
        {
            Name = name;
            IsSystem = isSystem;
            OwnerId = ownerId;
            ParentId = parentId;
            SortOrder = sortOrder;
        }

        [JsonConstructor]
        protected ChatFolder()
        {
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateParent(Guid? parentId)
        {
            ParentId = parentId;
        }

        public void UpdateSortOrder(int sortOrder)
        {
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Display name of the folder
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// True = system folder (visible to all users, managed by staff/admin).
        /// False = personal folder (only visible to the owner).
        /// </summary>
        public bool IsSystem { get; private set; }

        /// <summary>
        /// Owner of the folder. NULL for system folders.
        /// </summary>
        public Guid? OwnerId { get; private set; }
        public virtual User Owner { get; private set; }

        /// <summary>
        /// Parent folder for nesting. NULL for top-level folders.
        /// </summary>
        public Guid? ParentId { get; private set; }
        public virtual ChatFolder Parent { get; private set; }

        /// <summary>
        /// Sort order within the same level
        /// </summary>
        public int SortOrder { get; private set; }

        // Navigation properties
        public virtual ICollection<ChatFolder> Children { get; private set; } = new HashSet<ChatFolder>();
        public virtual ICollection<ChatFolderRoomAssignment> RoomAssignments { get; private set; } = new HashSet<ChatFolderRoomAssignment>();
    }
}
