namespace Orso.Arpa.Domain.ChatDomain.Enums
{
    public enum ChatRoomType
    {
        /// <summary>
        /// 1:1 private messages between two users
        /// </summary>
        Direct = 0,

        /// <summary>
        /// Group chat for project participants
        /// </summary>
        Project = 1,

        /// <summary>
        /// Global chat room (e.g., for all performers)
        /// </summary>
        Global = 2,

        /// <summary>
        /// Chat room for TODO item comments/discussion
        /// </summary>
        Todo = 3,

        /// <summary>
        /// Chat room linked to an entity (Ticket, Person, Appointment, etc.)
        /// </summary>
        Entity = 4,

        /// <summary>
        /// Manually created group chat (not tied to a project)
        /// </summary>
        Group = 5
    }
}
