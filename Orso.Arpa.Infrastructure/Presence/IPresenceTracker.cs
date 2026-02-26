using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orso.Arpa.Infrastructure.Presence
{
    public interface IPresenceTracker
    {
        Task<bool> UserConnected(OnlineUserDto user, string connectionId);
        Task<bool> UserDisconnected(Guid userId, string connectionId);
        Task<OnlineUserDto[]> GetOnlineUsers();
        Task<OnlineUserDto[]> GetRecentlyOnlineUsers(TimeSpan window);
        Task<OnlineUserDto?> GetUser(Guid userId);
        Task UpdateHeartbeat(Guid userId, bool isActive);
        Task<List<Guid>> CleanupStaleConnections(TimeSpan timeout);
    }
}
