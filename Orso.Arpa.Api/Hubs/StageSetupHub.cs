using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Orso.Arpa.Infrastructure.Presence;

namespace Orso.Arpa.Api.Hubs
{
    /// <summary>
    /// SignalR Hub for real-time collaboration on stage setups
    /// </summary>
    [Authorize]
    public class StageSetupHub : Hub
    {
        private readonly IStageSetupPresenceTracker _presenceTracker;

        public StageSetupHub(IStageSetupPresenceTracker presenceTracker)
        {
            _presenceTracker = presenceTracker;
        }

        /// <summary>
        /// Client joins a stage setup to receive real-time updates
        /// </summary>
        public async Task JoinStageSetup(Guid setupId)
        {
            var userId = GetUserId();
            var personId = GetPersonId();
            var displayName = GetDisplayName();

            if (userId == Guid.Empty)
            {
                return;
            }

            var editor = new StageSetupEditorDto
            {
                UserId = userId,
                PersonId = personId,
                DisplayName = displayName,
                JoinedAt = DateTime.UtcNow
            };

            // Leave previous setup if any
            var previousSetupId = await _presenceTracker.GetSetupIdForConnection(Context.ConnectionId);
            if (previousSetupId.HasValue && previousSetupId.Value != setupId)
            {
                await LeaveStageSetupInternal(previousSetupId.Value, userId);
            }

            var isFirstConnection = await _presenceTracker.EditorJoined(setupId, editor, Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(setupId));

            if (isFirstConnection)
            {
                await Clients.OthersInGroup(GetGroupName(setupId)).SendAsync("EditorJoined", editor);
            }

            // Send current editors to the joining client
            var editors = await _presenceTracker.GetEditors(setupId);
            await Clients.Caller.SendAsync("EditorsList", editors);
        }

        /// <summary>
        /// Client leaves a stage setup
        /// </summary>
        public async Task LeaveStageSetup(Guid setupId)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return;
            }

            await LeaveStageSetupInternal(setupId, userId);
        }

        /// <summary>
        /// Broadcasts a position update to all clients viewing the setup
        /// </summary>
        public async Task BroadcastPositionUpdated(Guid setupId, StageSetupPositionUpdateDto position)
        {
            await Clients.OthersInGroup(GetGroupName(setupId)).SendAsync("PositionUpdated", position);
        }

        /// <summary>
        /// Broadcasts that a position was removed
        /// </summary>
        public async Task BroadcastPositionRemoved(Guid setupId, Guid musicianProfileId)
        {
            await Clients.OthersInGroup(GetGroupName(setupId)).SendAsync("PositionRemoved", musicianProfileId);
        }

        /// <summary>
        /// Broadcasts that the setup metadata was modified
        /// </summary>
        public async Task BroadcastSetupModified(Guid setupId)
        {
            await Clients.OthersInGroup(GetGroupName(setupId)).SendAsync("SetupModified", setupId);
        }

        /// <summary>
        /// Notifies others that this user is currently moving a specific musician
        /// </summary>
        public async Task NotifyMoving(Guid setupId, Guid? musicianProfileId)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return;
            }

            await _presenceTracker.SetCurrentlyMoving(setupId, userId, musicianProfileId);

            await Clients.OthersInGroup(GetGroupName(setupId)).SendAsync("EditorMoving", new
            {
                UserId = userId,
                MusicianProfileId = musicianProfileId
            });
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetUserId();

            if (userId != Guid.Empty)
            {
                var result = await _presenceTracker.RemoveConnectionFromAllSetups(userId, Context.ConnectionId);

                if (result.HasValue && result.Value.WasLastConnection)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(result.Value.SetupId));
                    await Clients.Group(GetGroupName(result.Value.SetupId)).SendAsync("EditorLeft", userId);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        private async Task LeaveStageSetupInternal(Guid setupId, Guid userId)
        {
            var isLastConnection = await _presenceTracker.EditorLeft(setupId, userId, Context.ConnectionId);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(setupId));

            if (isLastConnection)
            {
                await Clients.Group(GetGroupName(setupId)).SendAsync("EditorLeft", userId);
            }
        }

        private static string GetGroupName(Guid setupId) => $"stage-setup-{setupId}";

        private Guid GetUserId()
        {
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
    }

    /// <summary>
    /// DTO for broadcasting position updates via SignalR
    /// </summary>
    public class StageSetupPositionUpdateDto
    {
        public Guid Id { get; set; }
        public Guid MusicianProfileId { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public int? Row { get; set; }
        public int? Stand { get; set; }
    }
}
