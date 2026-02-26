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
        private readonly Dictionary<Guid, OnlineUserDto> _recentlyOffline = new();

        /// <summary>
        /// Adds a user connection. Returns true if this is the user's first connection (user just came online).
        /// </summary>
        public Task<bool> UserConnected(OnlineUserDto user, string connectionId)
        {
            var isFirstConnection = false;

            lock (_lock)
            {
                // Remove from recently offline if reconnecting
                _recentlyOffline.Remove(user.UserId);

                if (_onlineUsers.TryGetValue(user.UserId, out var existing))
                {
                    existing.ConnectionIds.Add(connectionId);
                }
                else
                {
                    user.LastHeartbeat = DateTime.UtcNow;
                    user.Status = "active";
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
                        // Move to recently offline with LastSeenAt timestamp
                        var offlineUser = new OnlineUserDto
                        {
                            UserId = existing.User.UserId,
                            PersonId = existing.User.PersonId,
                            DisplayName = existing.User.DisplayName,
                            InstrumentName = existing.User.InstrumentName,
                            ConnectedAt = existing.User.ConnectedAt,
                            LastSeenAt = DateTime.UtcNow
                        };
                        _recentlyOffline[userId] = offlineUser;

                        _onlineUsers.Remove(userId);
                        isLastConnection = true;

                        // Cleanup: remove entries older than 2 hours
                        var cutoff = DateTime.UtcNow.AddHours(-2);
                        var stale = _recentlyOffline
                            .Where(kv => kv.Value.LastSeenAt < cutoff)
                            .Select(kv => kv.Key)
                            .ToList();
                        foreach (var key in stale)
                        {
                            _recentlyOffline.Remove(key);
                        }
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
        /// Gets users who went offline within the specified time window.
        /// </summary>
        public Task<OnlineUserDto[]> GetRecentlyOnlineUsers(TimeSpan window)
        {
            OnlineUserDto[] users;

            lock (_lock)
            {
                var cutoff = DateTime.UtcNow - window;
                users = _recentlyOffline.Values
                    .Where(u => u.LastSeenAt >= cutoff)
                    .OrderByDescending(u => u.LastSeenAt)
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

        /// <summary>
        /// Updates the heartbeat timestamp and activity status for a user.
        /// </summary>
        public Task UpdateHeartbeat(Guid userId, bool isActive)
        {
            lock (_lock)
            {
                if (_onlineUsers.TryGetValue(userId, out var existing))
                {
                    existing.User.LastHeartbeat = DateTime.UtcNow;
                    existing.User.Status = isActive ? "active" : "idle";
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Removes users whose last heartbeat is older than the timeout. Returns removed user IDs.
        /// </summary>
        public Task<List<Guid>> CleanupStaleConnections(TimeSpan timeout)
        {
            var removedUserIds = new List<Guid>();

            lock (_lock)
            {
                var cutoff = DateTime.UtcNow - timeout;
                var staleUsers = _onlineUsers
                    .Where(kv => kv.Value.User.LastHeartbeat < cutoff)
                    .Select(kv => kv.Key)
                    .ToList();

                foreach (var userId in staleUsers)
                {
                    if (_onlineUsers.TryGetValue(userId, out var existing))
                    {
                        var offlineUser = new OnlineUserDto
                        {
                            UserId = existing.User.UserId,
                            PersonId = existing.User.PersonId,
                            DisplayName = existing.User.DisplayName,
                            InstrumentName = existing.User.InstrumentName,
                            ConnectedAt = existing.User.ConnectedAt,
                            LastSeenAt = DateTime.UtcNow
                        };
                        _recentlyOffline[userId] = offlineUser;
                        _onlineUsers.Remove(userId);
                        removedUserIds.Add(userId);
                    }
                }
            }

            return Task.FromResult(removedUserIds);
        }
    }
}
