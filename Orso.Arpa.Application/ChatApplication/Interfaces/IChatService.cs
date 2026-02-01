using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Orso.Arpa.Application.ChatApplication.Model;

namespace Orso.Arpa.Application.ChatApplication.Interfaces
{
    public interface IChatService
    {
        #region Chat Room Operations

        /// <summary>
        /// Get all chat rooms for the current user
        /// </summary>
        Task<ChatRoomListDto> GetUserChatRoomsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a specific chat room
        /// </summary>
        Task<ChatRoomDto> GetChatRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create or get existing direct chat with another user
        /// </summary>
        Task<ChatRoomDto> CreateDirectChatAsync(Guid otherUserId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get or create project chat room
        /// </summary>
        Task<ChatRoomDto> GetOrCreateProjectChatAsync(Guid projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get or create global chat room
        /// </summary>
        Task<ChatRoomDto> GetOrCreateGlobalChatAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if current user is member of a room
        /// </summary>
        Task<bool> IsUserMemberOfRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add user to a chat room
        /// </summary>
        Task AddUserToRoomAsync(Guid roomId, Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove user from a chat room
        /// </summary>
        Task RemoveUserFromRoomAsync(Guid roomId, Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Mute/unmute a chat room for the current user
        /// </summary>
        Task<bool> MuteRoomAsync(Guid roomId, bool mute, CancellationToken cancellationToken = default);

        /// <summary>
        /// Invite users to a chat room
        /// </summary>
        Task<ChatInviteResultDto> InviteUsersToRoomAsync(Guid roomId, InviteUsersToChatDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get members of a chat room
        /// </summary>
        Task<List<ChatMemberDto>> GetRoomMembersAsync(Guid roomId, CancellationToken cancellationToken = default);

        #endregion

        #region Message Operations

        /// <summary>
        /// Send a new message
        /// </summary>
        Task<ChatMessageDto> SendMessageAsync(Guid roomId, SendMessageDto messageDto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send a message with file attachments
        /// </summary>
        Task<ChatMessageDto> SendMessageWithAttachmentsAsync(Guid roomId, string content, Guid? replyToMessageId, List<(string fileName, string contentType, byte[] data)> files, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get messages for a chat room with pagination
        /// </summary>
        Task<List<ChatMessageDto>> GetMessagesAsync(Guid roomId, int pageSize = 50, DateTime? before = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a single message by ID
        /// </summary>
        Task<ChatMessageDto> GetMessageAsync(Guid messageId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a message (soft delete)
        /// </summary>
        Task<bool> DeleteMessageAsync(Guid messageId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Edit a message (only within 15 minute window)
        /// </summary>
        Task<ChatMessageDto> EditMessageAsync(Guid messageId, string newContent, CancellationToken cancellationToken = default);

        #endregion

        #region Read Status

        /// <summary>
        /// Mark all messages in a room as read
        /// </summary>
        Task MarkRoomAsReadAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get unread count for a room
        /// </summary>
        Task<int> GetUnreadCountAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get total unread count across all rooms
        /// </summary>
        Task<int> GetTotalUnreadCountAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Reactions

        /// <summary>
        /// Toggle a reaction on a message (add if not exists, remove if exists)
        /// </summary>
        Task<(bool added, List<MessageReactionDto> reactions)> ToggleReactionAsync(Guid messageId, string emoji, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all reactions for a message
        /// </summary>
        Task<List<MessageReactionDto>> GetReactionsAsync(Guid messageId, CancellationToken cancellationToken = default);

        #endregion

        #region Attachments

        /// <summary>
        /// Upload attachment to a message
        /// </summary>
        Task<ChatAttachmentDto> UploadAttachmentAsync(Guid roomId, Guid messageId, string fileName, string contentType, byte[] fileData, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get download URL for an attachment
        /// </summary>
        Task<string> GetAttachmentDownloadUrlAsync(Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get attachment file content for download
        /// </summary>
        Task<(byte[] content, string fileName, string contentType)?> GetAttachmentFileAsync(Guid attachmentId, CancellationToken cancellationToken = default);

        #endregion
    }
}
