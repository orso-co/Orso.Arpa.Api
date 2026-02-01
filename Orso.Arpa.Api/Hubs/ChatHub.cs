using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Orso.Arpa.Application.ChatApplication.Interfaces;
using Orso.Arpa.Application.ChatApplication.Model;

namespace Orso.Arpa.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        #region Connection Management

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                await base.OnConnectedAsync();
                return;
            }

            // Join all chat rooms the user is a member of
            var rooms = await _chatService.GetUserChatRoomsAsync();
            foreach (var room in rooms.Rooms)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{room.Id}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // SignalR automatically removes connection from all groups on disconnect
            await base.OnDisconnectedAsync(exception);
        }

        #endregion

        #region Room Management

        /// <summary>
        /// Join a specific chat room to receive messages
        /// </summary>
        public async Task JoinRoom(Guid roomId)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return;

            // Verify user is member of the room
            if (!await _chatService.IsUserMemberOfRoomAsync(roomId))
                return;

            await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{roomId}");

            // Notify others that user joined
            await Clients.OthersInGroup($"chat_{roomId}").SendAsync("UserJoinedRoom", new
            {
                RoomId = roomId,
                UserId = userId,
                UserName = GetDisplayName()
            });
        }

        /// <summary>
        /// Leave a specific chat room
        /// </summary>
        public async Task LeaveRoom(Guid roomId)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return;

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"chat_{roomId}");

            // Notify others that user left
            await Clients.OthersInGroup($"chat_{roomId}").SendAsync("UserLeftRoom", new
            {
                RoomId = roomId,
                UserId = userId
            });
        }

        #endregion

        #region Typing Indicator

        /// <summary>
        /// Notify others that user is typing
        /// </summary>
        public async Task StartTyping(Guid roomId)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return;

            // Verify user is member of the room
            if (!await _chatService.IsUserMemberOfRoomAsync(roomId))
                return;

            await Clients.OthersInGroup($"chat_{roomId}").SendAsync("UserTyping", new TypingIndicatorDto
            {
                ChatRoomId = roomId,
                UserId = userId,
                UserName = GetDisplayName(),
                IsTyping = true
            });
        }

        /// <summary>
        /// Notify others that user stopped typing
        /// </summary>
        public async Task StopTyping(Guid roomId)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return;

            await Clients.OthersInGroup($"chat_{roomId}").SendAsync("UserTyping", new TypingIndicatorDto
            {
                ChatRoomId = roomId,
                UserId = userId,
                UserName = GetDisplayName(),
                IsTyping = false
            });
        }

        #endregion

        #region Message Operations

        /// <summary>
        /// Send a message to a chat room (alternative to REST endpoint)
        /// </summary>
        public async Task SendMessage(Guid roomId, string content, Guid? replyToMessageId = null)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return;

            try
            {
                var message = await _chatService.SendMessageAsync(roomId, new SendMessageDto
                {
                    Content = content,
                    ReplyToMessageId = replyToMessageId
                });

                // Broadcast to all users in the room
                await Clients.Group($"chat_{roomId}").SendAsync("ReceiveMessage", message);
            }
            catch (Exception)
            {
                // Send error back to caller
                await Clients.Caller.SendAsync("MessageError", new { Error = "Failed to send message" });
            }
        }

        /// <summary>
        /// Mark a room as read
        /// </summary>
        public async Task MarkAsRead(Guid roomId)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return;

            await _chatService.MarkRoomAsReadAsync(roomId);

            // Optionally notify others (for read receipts)
            await Clients.OthersInGroup($"chat_{roomId}").SendAsync("MessageRead", new
            {
                RoomId = roomId,
                UserId = userId,
                ReadAt = DateTime.UtcNow
            });
        }

        #endregion

        #region Helper Methods

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

        private string GetDisplayName()
        {
            return Context.User?.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? "Unknown";
        }

        #endregion
    }
}
