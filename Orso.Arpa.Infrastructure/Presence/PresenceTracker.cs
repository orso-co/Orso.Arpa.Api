using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orso.Arpa.Infrastructure.Presence
{
    public class PresenceTracker : IPresenceTracker
    {
        private readonly object _lock = new();
        private readonly Dictionary<Guid, (OnlineUserDto User, HashSet<string> ConnectionIds)> _onlineUsers = new();

        /// <summary>
        /// Adds a user connection. Returns true if this is the user's first connection (user just came online).
        /// </summary>
        public Task<bool> UserConnected(OnlineUserDto user, string connectionId)
        {
            var isFirstConnection = false;

            lock (_lock)
            {
                if (_onlineUsers.TryGetValue(user.UserId, out var existing))
                {
                    existing.ConnectionIds.Add(connectionId);
                }
                else
                {
                    _onlineUsers[user.UserId] = (user, new HashSet<string> { connectionId });
                    isFirstConnection = true;
                }
            }

            return Task.FromResult(isFirstConnection);
        }

        /// <summary>
        /// Removes a user connection. Returns true if this was the user's last connection (user went offline).
        /// </summary>
        public Task<bool> UserDisconnected(Guid userId, string connectionId)
        {
            var isLastConnection = false;

            lock (_lock)
            {
                if (_onlineUsers.TryGetValue(userId, out var existing))
                {
                    existing.ConnectionIds.Remove(connectionId);

                    if (existing.ConnectionIds.Count == 0)
                    {
                        _onlineUsers.Remove(userId);
                        isLastConnection = true;
                    }
                }
            }

            return Task.FromResult(isLastConnection);
        }

        /// <summary>
        /// Gets all currently online users.
        /// </summary>
        public Task<OnlineUserDto[]> GetOnlineUsers()
        {
            OnlineUserDto[] users;

            lock (_lock)
            {
                users = _onlineUsers.Values
                    .Select(x => x.User)
                    .OrderBy(x => x.DisplayName)
                    .ToArray();
            }

            return Task.FromResult(users);
        }

        /// <summary>
        /// Gets a specific user if they are online.
        /// </summary>
        public Task<OnlineUserDto?> GetUser(Guid userId)
        {
            OnlineUserDto? user = null;

            lock (_lock)
            {
                if (_onlineUsers.TryGetValue(userId, out var existing))
                {
                    user = existing.User;
                }
            }

            return Task.FromResult(user);
        }
    }
}
