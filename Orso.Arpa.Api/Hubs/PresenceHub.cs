using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Infrastructure.Presence;

namespace Orso.Arpa.Api.Hubs
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly IPresenceTracker _presenceTracker;
        private readonly IArpaContext _arpaContext;

        public PresenceHub(IPresenceTracker presenceTracker, IArpaContext arpaContext)
        {
            _presenceTracker = presenceTracker;
            _arpaContext = arpaContext;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();
            var personId = GetPersonId();
            var displayName = GetDisplayName();

            if (userId == Guid.Empty || personId == Guid.Empty)
            {
                await base.OnConnectedAsync();
                return;
            }

            var instrumentName = await GetMainInstrumentNameAsync(personId);

            var userDto = new OnlineUserDto
            {
                UserId = userId,
                PersonId = personId,
                DisplayName = displayName,
                InstrumentName = instrumentName,
                ConnectedAt = DateTime.UtcNow
            };

            var isFirstConnection = await _presenceTracker.UserConnected(userDto, Context.ConnectionId);

            if (isFirstConnection)
            {
                await Clients.Others.SendAsync("UserIsOnline", userDto);
            }

            var onlineUsers = await _presenceTracker.GetOnlineUsers();
            await Clients.Caller.SendAsync("OnlineUsersList", onlineUsers);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetUserId();

            if (userId != Guid.Empty)
            {
                var isLastConnection = await _presenceTracker.UserDisconnected(userId, Context.ConnectionId);

                if (isLastConnection)
                {
                    await Clients.Others.SendAsync("UserIsOffline", new { userId });
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        private Guid GetUserId()
        {
            var userIdClaim = Context.User?.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
        }

        private Guid GetPersonId()
        {
            var personIdClaim = Context.User?.Claims?.FirstOrDefault(c => c.Type.Contains("/person_id"))?.Value;
            return Guid.TryParse(personIdClaim, out var personId) ? personId : Guid.Empty;
        }

        private string GetDisplayName()
        {
            return Context.User?.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? "Unknown";
        }

        private async Task<string?> GetMainInstrumentNameAsync(Guid personId)
        {
            var mainProfile = await _arpaContext.MusicianProfiles
                .AsNoTracking()
                .Include(mp => mp.Instrument)
                .Where(mp => mp.PersonId == personId && mp.IsMainProfile && !mp.Deleted)
                .FirstOrDefaultAsync();

            return mainProfile?.Instrument?.Name;
        }
    }
}
