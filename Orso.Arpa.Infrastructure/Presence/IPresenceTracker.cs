using System;
using System.Threading.Tasks;

namespace Orso.Arpa.Infrastructure.Presence
{
    public interface IPresenceTracker
    {
        Task<bool> UserConnected(OnlineUserDto user, string connectionId);
        Task<bool> UserDisconnected(Guid userId, string connectionId);
        Task<OnlineUserDto[]> GetOnlineUsers();
        Task<OnlineUserDto?> GetUser(Guid userId);
    }
}
