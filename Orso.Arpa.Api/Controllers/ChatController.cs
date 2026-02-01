using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Orso.Arpa.Api.Hubs;
using Orso.Arpa.Application.ChatApplication.Interfaces;
using Orso.Arpa.Application.ChatApplication.Model;

namespace Orso.Arpa.Api.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _chatHubContext;

        public ChatController(
            IChatService chatService,
            IHubContext<ChatHub> chatHubContext)
        {
            _chatService = chatService;
            _chatHubContext = chatHubContext;
        }

        #region Chat Rooms

        /// <summary>
        /// Gets all chat rooms for the current user.
        /// </summary>
        /// <returns>List of chat rooms with unread counts</returns>
        [HttpGet("rooms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatRoomListDto>> GetRooms()
        {
            return Ok(await _chatService.GetUserChatRoomsAsync());
        }

        /// <summary>
        /// Gets a specific chat room.
        /// </summary>
        /// <param name="roomId">Chat room ID</param>
        /// <returns>The chat room</returns>
        [HttpGet("rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChatRoomDto>> GetRoom(Guid roomId)
        {
            var room = await _chatService.GetChatRoomAsync(roomId);
            if (room == null)
                return NotFound("Chat room not found or you are not a member");

            return Ok(room);
        }

        /// <summary>
        /// Creates or gets an existing direct chat with another user.
        /// </summary>
        /// <param name="dto">Other user ID</param>
        /// <returns>The chat room</returns>
        [HttpPost("rooms/direct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChatRoomDto>> CreateDirectChat([FromBody] CreateDirectChatDto dto)
        {
            try
            {
                var room = await _chatService.CreateDirectChatAsync(dto.OtherPersonId);
                return Ok(room);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets or creates a project chat room.
        /// </summary>
        /// <param name="dto">Project ID</param>
        /// <returns>The chat room</returns>
        [HttpPost("rooms/project")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChatRoomDto>> GetOrCreateProjectChat([FromBody] CreateProjectChatDto dto)
        {
            try
            {
                var room = await _chatService.GetOrCreateProjectChatAsync(dto.ProjectId);
                return Ok(room);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets or creates the global chat room.
        /// </summary>
        /// <returns>The global chat room</returns>
        [HttpPost("rooms/global")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatRoomDto>> GetOrCreateGlobalChat()
        {
            var room = await _chatService.GetOrCreateGlobalChatAsync();
            return Ok(room);
        }

        /// <summary>
        /// Gets members of a chat room.
        /// </summary>
        /// <param name="roomId">Chat room ID</param>
        /// <returns>List of members</returns>
        [HttpGet("rooms/{roomId}/members")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<ChatMemberDto>>> GetRoomMembers(Guid roomId)
        {
            if (!await _chatService.IsUserMemberOfRoomAsync(roomId))
                return Forbid();

            var members = await _chatService.GetRoomMembersAsync(roomId);
            return Ok(members);
        }

        /// <summary>
        /// Invites users to a chat room.
        /// </summary>
        /// <param name="roomId">Chat room ID</param>
        /// <param name="dto">User IDs to invite</param>
        /// <returns>Invite result</returns>
        [HttpPost("rooms/{roomId}/invite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChatInviteResultDto>> InviteUsers(Guid roomId, [FromBody] InviteUsersToChatDto dto)
        {
            try
            {
                var result = await _chatService.InviteUsersToRoomAsync(roomId, dto);

                // Notify via SignalR
                if (result.NewMembers.Count > 0)
                {
                    await _chatHubContext.Clients.Group($"chat_{roomId}").SendAsync("MembersAdded", new
                    {
                        RoomId = roomId,
                        NewMembers = result.NewMembers
                    });
                }

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Leaves a chat room.
        /// </summary>
        /// <param name="roomId">Chat room ID</param>
        [HttpDelete("rooms/{roomId}/leave")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> LeaveRoom(Guid roomId)
        {
            // Get current user ID from the service (it uses IUserAccessor internally)
            var room = await _chatService.GetChatRoomAsync(roomId);
            if (room != null)
            {
                // The service will use the current user's ID
                await _chatService.RemoveUserFromRoomAsync(roomId, Guid.Empty); // Service will use current user
            }
            return NoContent();
        }

        /// <summary>
        /// Mutes or unmutes a chat room.
        /// </summary>
        /// <param name="roomId">Chat room ID</param>
        /// <param name="mute">True to mute, false to unmute</param>
        [HttpPost("rooms/{roomId}/mute")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MuteRoom(Guid roomId, [FromQuery] bool mute = true)
        {
            var success = await _chatService.MuteRoomAsync(roomId, mute);
            if (!success)
                return NotFound("Chat room not found or you are not a member");

            return NoContent();
        }

        #endregion

        #region Messages

        /// <summary>
        /// Gets messages for a chat room (with pagination).
        /// </summary>
        /// <param name="roomId">Chat room ID</param>
        /// <param name="pageSize">Number of messages to return (default: 50)</param>
        /// <param name="before">Get messages before this timestamp</param>
        /// <returns>List of messages</returns>
        [HttpGet("rooms/{roomId}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<ChatMessageDto>>> GetMessages(
            Guid roomId,
            [FromQuery] int pageSize = 50,
            [FromQuery] DateTime? before = null)
        {
            try
            {
                var messages = await _chatService.GetMessagesAsync(roomId, pageSize, before);
                return Ok(messages);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Sends a message to a chat room.
        /// </summary>
        /// <param name="roomId">Chat room ID</param>
        /// <param name="dto">Message content</param>
        /// <returns>The sent message</returns>
        [HttpPost("rooms/{roomId}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ChatMessageDto>> SendMessage(Guid roomId, [FromBody] SendMessageDto dto)
        {
            try
            {
                var message = await _chatService.SendMessageAsync(roomId, dto);

                // Broadcast via SignalR
                await _chatHubContext.Clients.Group($"chat_{roomId}").SendAsync("ReceiveMessage", message);

                return Ok(message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a message (soft delete, only own messages).
        /// </summary>
        /// <param name="messageId">Message ID</param>
        [HttpDelete("messages/{messageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMessage(Guid messageId)
        {
            var success = await _chatService.DeleteMessageAsync(messageId);
            if (!success)
                return NotFound("Message not found or you cannot delete it");

            // Get the message to find room ID for SignalR notification
            var message = await _chatService.GetMessageAsync(messageId);
            if (message != null)
            {
                await _chatHubContext.Clients.Group($"chat_{message.ChatRoomId}").SendAsync("MessageDeleted", messageId);
            }

            return NoContent();
        }

        /// <summary>
        /// Edits a message (only own messages, within 15 minutes).
        /// </summary>
        /// <param name="messageId">Message ID</param>
        /// <param name="dto">New content</param>
        /// <returns>The edited message</returns>
        [HttpPut("messages/{messageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChatMessageDto>> EditMessage(Guid messageId, [FromBody] SendMessageDto dto)
        {
            var message = await _chatService.EditMessageAsync(messageId, dto.Content);
            if (message == null)
                return NotFound("Message not found or you cannot edit it");

            // Broadcast via SignalR
            await _chatHubContext.Clients.Group($"chat_{message.ChatRoomId}").SendAsync("MessageEdited", message);

            return Ok(message);
        }

        #endregion

        #region Read Status

        /// <summary>
        /// Marks all messages in a room as read.
        /// </summary>
        /// <param name="roomId">Chat room ID</param>
        [HttpPost("rooms/{roomId}/read")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> MarkAsRead(Guid roomId)
        {
            await _chatService.MarkRoomAsReadAsync(roomId);
            return NoContent();
        }

        /// <summary>
        /// Gets total unread message count across all rooms.
        /// </summary>
        /// <returns>Unread count</returns>
        [HttpGet("unread-count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetTotalUnreadCount()
        {
            var count = await _chatService.GetTotalUnreadCountAsync();
            return Ok(count);
        }

        #endregion

        #region Reactions

        /// <summary>
        /// Toggles a reaction on a message (add if not exists, remove if exists).
        /// </summary>
        /// <param name="messageId">Message ID</param>
        /// <param name="dto">Emoji to react with</param>
        /// <returns>Updated reactions</returns>
        [HttpPost("messages/{messageId}/reactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MessageReactionDto>>> ToggleReaction(Guid messageId, [FromBody] ToggleReactionDto dto)
        {
            try
            {
                var (added, reactions) = await _chatService.ToggleReactionAsync(messageId, dto.Emoji);

                // Get message to find room ID for SignalR broadcast
                var message = await _chatService.GetMessageAsync(messageId);
                if (message != null)
                {
                    await _chatHubContext.Clients.Group($"chat_{message.ChatRoomId}").SendAsync("ReactionUpdated", new ReactionUpdateDto
                    {
                        MessageId = messageId,
                        ChatRoomId = message.ChatRoomId,
                        Emoji = dto.Emoji,
                        Added = added,
                        Reactions = reactions
                    });
                }

                return Ok(reactions);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Gets all reactions for a message.
        /// </summary>
        /// <param name="messageId">Message ID</param>
        /// <returns>List of reactions grouped by emoji</returns>
        [HttpGet("messages/{messageId}/reactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MessageReactionDto>>> GetReactions(Guid messageId)
        {
            var reactions = await _chatService.GetReactionsAsync(messageId);
            return Ok(reactions);
        }

        #endregion

        #region Attachments

        /// <summary>
        /// Sends a message with file attachments.
        /// </summary>
        /// <param name="roomId">Chat room ID</param>
        /// <param name="content">Optional message content</param>
        /// <param name="replyToMessageId">Optional message ID to reply to</param>
        /// <param name="files">File attachments</param>
        /// <returns>The sent message with attachments</returns>
        [HttpPost("rooms/{roomId}/messages/attachments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ChatMessageDto>> SendMessageWithAttachments(
            Guid roomId,
            [FromForm] string content,
            [FromForm] Guid? replyToMessageId,
            [FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("At least one file is required");
            }

            try
            {
                // Convert files to tuples
                var fileData = new List<(string fileName, string contentType, byte[] data)>();
                foreach (var file in files)
                {
                    using var memoryStream = new System.IO.MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    fileData.Add((file.FileName, file.ContentType, memoryStream.ToArray()));
                }

                var message = await _chatService.SendMessageWithAttachmentsAsync(roomId, content, replyToMessageId, fileData);

                // Broadcast via SignalR
                await _chatHubContext.Clients.Group($"chat_{roomId}").SendAsync("ReceiveMessage", message);

                return Ok(message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Downloads an attachment.
        /// </summary>
        /// <param name="attachmentId">Attachment ID</param>
        /// <returns>The file content</returns>
        [HttpGet("attachments/{attachmentId}/download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DownloadAttachment(Guid attachmentId)
        {
            var result = await _chatService.GetAttachmentFileAsync(attachmentId);

            if (result == null)
            {
                return NotFound("Attachment not found or you don't have access");
            }

            var (content, fileName, contentType) = result.Value;
            return File(content, contentType, fileName);
        }

        #endregion
    }
}
