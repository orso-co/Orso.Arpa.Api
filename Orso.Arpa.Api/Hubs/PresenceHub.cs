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

            Console.WriteLine($"[PresenceHub] OnConnectedAsync - UserId: {userId}, PersonId: {personId}, DisplayName: {displayName}");
            Console.WriteLine($"[PresenceHub] Claims: {string.Join(", ", Context.User?.Claims?.Select(c => $"{c.Type}={c.Value}") ?? Array.Empty<string>())}");

            if (userId == Guid.Empty || personId == Guid.Empty)
            {
                Console.WriteLine($"[PresenceHub] Early return - UserId or PersonId is empty");
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
            Console.WriteLine($"[PresenceHub] User tracked - IsFirstConnection: {isFirstConnection}");

            if (isFirstConnection)
            {
                await Clients.Others.SendAsync("UserIsOnline", userDto);
                Console.WriteLine($"[PresenceHub] Sent UserIsOnline to others");
            }

            var onlineUsers = await _presenceTracker.GetOnlineUsers();
            Console.WriteLine($"[PresenceHub] Sending OnlineUsersList with {onlineUsers.Length} users");
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
            // There are multiple nameidentifier claims - one is the username, one is the GUID
            // Find the one that is a valid GUID
            var nameIdentifierClaims = Context.User?.Claims?
                .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                .Select(c => c.Value);

            foreach (var claim in nameIdentifierClaims ?? Enumerable.Empty<string>())
            {
                if (Guid.TryParse(claim, out var userId))
                {
                    return userId;
                }
            }
            return Guid.Empty;
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
