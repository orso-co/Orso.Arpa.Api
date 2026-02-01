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
        Global = 2
    }
}
