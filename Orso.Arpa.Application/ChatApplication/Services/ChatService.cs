using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Application.ChatApplication.Interfaces;
using Orso.Arpa.Application.ChatApplication.Model;
using Orso.Arpa.Domain.ChatDomain.Enums;
using Orso.Arpa.Domain.ChatDomain.Interfaces;
using Orso.Arpa.Domain.ChatDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Infrastructure.Presence;

namespace Orso.Arpa.Application.ChatApplication.Services
{
    public class ChatService : IChatService
    {
        private readonly IArpaContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IPresenceTracker _presenceTracker;
        private readonly IChatAttachmentFileAccessor _fileAccessor;
        private readonly ILogger<ChatService> _logger;

        // Edit window: 15 minutes
        private static readonly TimeSpan EditWindowDuration = TimeSpan.FromMinutes(15);

        public ChatService(
            IArpaContext context,
            IUserAccessor userAccessor,
            IPresenceTracker presenceTracker,
            IChatAttachmentFileAccessor fileAccessor,
            ILogger<ChatService> logger)
        {
            _context = context;
            _userAccessor = userAccessor;
            _presenceTracker = presenceTracker;
            _fileAccessor = fileAccessor;
            _logger = logger;
        }

        #region Chat Room Operations

        public async Task<ChatRoomListDto> GetUserChatRoomsAsync(CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            var rooms = await _context.ChatRoomMembers
                .Where(m => m.UserId == userId && !m.Deleted)
                .Select(m => m.ChatRoom)
                .Where(r => !r.Deleted)
                .OrderByDescending(r => r.LastMessageAt ?? r.CreatedAt)
                .ToListAsync(cancellationToken);

            var roomDtos = new List<ChatRoomDto>();

            foreach (var room in rooms)
            {
                var unreadCount = await GetUnreadCountForRoomAsync(room.Id, userId, cancellationToken);
                var lastMessage = await GetLastMessageAsync(room.Id, cancellationToken);

                roomDtos.Add(new ChatRoomDto
                {
                    Id = room.Id,
                    Type = (ChatRoomTypeDto)(int)room.Type,
                    Name = await GetRoomDisplayNameAsync(room, userId, cancellationToken),
                    ProjectId = room.ProjectId,
                    ProjectTitle = room.Project?.Title,
                    CreatedAt = room.CreatedAt,
                    LastMessageAt = room.LastMessageAt,
                    LastMessage = lastMessage,
                    UnreadCount = unreadCount,
                    Members = await GetRoomMembersInternalAsync(room.Id, cancellationToken)
                });
            }

            return new ChatRoomListDto
            {
                Rooms = roomDtos,
                TotalUnread = roomDtos.Sum(r => r.UnreadCount)
            };
        }

        public async Task<ChatRoomDto> GetChatRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            var membership = await _context.ChatRoomMembers
                .FirstOrDefaultAsync(m => m.ChatRoomId == roomId && m.UserId == userId && !m.Deleted, cancellationToken);

            if (membership == null)
                return null;

            var room = await _context.ChatRooms
                .FirstOrDefaultAsync(r => r.Id == roomId && !r.Deleted, cancellationToken);

            if (room == null)
                return null;

            var unreadCount = await GetUnreadCountForRoomAsync(roomId, userId, cancellationToken);

            return new ChatRoomDto
            {
                Id = room.Id,
                Type = (ChatRoomTypeDto)(int)room.Type,
                Name = await GetRoomDisplayNameAsync(room, userId, cancellationToken),
                ProjectId = room.ProjectId,
                ProjectTitle = room.Project?.Title,
                CreatedAt = room.CreatedAt,
                LastMessageAt = room.LastMessageAt,
                UnreadCount = unreadCount,
                Members = await GetRoomMembersInternalAsync(room.Id, cancellationToken)
            };
        }

        public async Task<ChatRoomDto> CreateDirectChatAsync(Guid otherPersonId, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            // Get the UserId for the other person
            var otherUser = await _context.Users
                .FirstOrDefaultAsync(u => u.PersonId == otherPersonId, cancellationToken)
                ?? throw new ArgumentException("Person has no user account");

            var otherUserId = otherUser.Id;

            // Check if direct chat already exists between these users
            var existingRoom = await _context.ChatRooms
                .Where(r => r.Type == ChatRoomType.Direct && !r.Deleted)
                .Where(r => r.Members.Any(m => m.UserId == userId && !m.Deleted) &&
                           r.Members.Any(m => m.UserId == otherUserId && !m.Deleted))
                .FirstOrDefaultAsync(cancellationToken);

            if (existingRoom != null)
            {
                return await GetChatRoomAsync(existingRoom.Id, cancellationToken);
            }

            // Create new direct chat room
            var room = new ChatRoom(Guid.NewGuid(), ChatRoomType.Direct);
            _context.ChatRooms.Add(room);

            // Add both users as members
            var member1 = new ChatRoomMember(Guid.NewGuid(), room.Id, userId);
            var member2 = new ChatRoomMember(Guid.NewGuid(), room.Id, otherUserId);

            _context.ChatRoomMembers.Add(member1);
            _context.ChatRoomMembers.Add(member2);

            await _context.SaveChangesAsync(cancellationToken);

            return await GetChatRoomAsync(room.Id, cancellationToken);
        }

        public async Task<ChatRoomDto> GetOrCreateProjectChatAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            // Check if project chat exists
            var existingRoom = await _context.ChatRooms
                .FirstOrDefaultAsync(r => r.Type == ChatRoomType.Project && r.ProjectId == projectId && !r.Deleted, cancellationToken);

            if (existingRoom != null)
            {
                // Add user to room if not already a member
                var isMember = await _context.ChatRoomMembers
                    .AnyAsync(m => m.ChatRoomId == existingRoom.Id && m.UserId == userId && !m.Deleted, cancellationToken);

                if (!isMember)
                {
                    var newMember = new ChatRoomMember(Guid.NewGuid(), existingRoom.Id, userId);
                    _context.ChatRoomMembers.Add(newMember);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return await GetChatRoomAsync(existingRoom.Id, cancellationToken);
            }

            // Create new project chat room
            var project = await _context.Projects.FindAsync(new object[] { projectId }, cancellationToken);
            if (project == null)
                throw new ArgumentException($"Project {projectId} not found");

            var room = new ChatRoom(Guid.NewGuid(), ChatRoomType.Project, project.Title, projectId);
            _context.ChatRooms.Add(room);

            var member = new ChatRoomMember(Guid.NewGuid(), room.Id, userId);
            _context.ChatRoomMembers.Add(member);

            await _context.SaveChangesAsync(cancellationToken);

            return await GetChatRoomAsync(room.Id, cancellationToken);
        }

        public async Task<ChatRoomDto> GetOrCreateGlobalChatAsync(CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            // Check if global chat exists
            var existingRoom = await _context.ChatRooms
                .FirstOrDefaultAsync(r => r.Type == ChatRoomType.Global && !r.Deleted, cancellationToken);

            if (existingRoom != null)
            {
                // Add user to room if not already a member
                var isMember = await _context.ChatRoomMembers
                    .AnyAsync(m => m.ChatRoomId == existingRoom.Id && m.UserId == userId && !m.Deleted, cancellationToken);

                if (!isMember)
                {
                    var newMember = new ChatRoomMember(Guid.NewGuid(), existingRoom.Id, userId);
                    _context.ChatRoomMembers.Add(newMember);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return await GetChatRoomAsync(existingRoom.Id, cancellationToken);
            }

            // Create global chat room
            var room = new ChatRoom(Guid.NewGuid(), ChatRoomType.Global, "Global Chat");
            _context.ChatRooms.Add(room);

            var member = new ChatRoomMember(Guid.NewGuid(), room.Id, userId);
            _context.ChatRoomMembers.Add(member);

            await _context.SaveChangesAsync(cancellationToken);

            return await GetChatRoomAsync(room.Id, cancellationToken);
        }

        public async Task<bool> IsUserMemberOfRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;
            return await _context.ChatRoomMembers
                .AnyAsync(m => m.ChatRoomId == roomId && m.UserId == userId && !m.Deleted, cancellationToken);
        }

        public async Task AddUserToRoomAsync(Guid roomId, Guid userId, CancellationToken cancellationToken = default)
        {
            var isMember = await _context.ChatRoomMembers
                .AnyAsync(m => m.ChatRoomId == roomId && m.UserId == userId && !m.Deleted, cancellationToken);

            if (isMember)
                return;

            var member = new ChatRoomMember(Guid.NewGuid(), roomId, userId);
            _context.ChatRoomMembers.Add(member);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveUserFromRoomAsync(Guid roomId, Guid userId, CancellationToken cancellationToken = default)
        {
            var member = await _context.ChatRoomMembers
                .FirstOrDefaultAsync(m => m.ChatRoomId == roomId && m.UserId == userId && !m.Deleted, cancellationToken);

            if (member != null)
            {
                _context.Remove(member);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> MuteRoomAsync(Guid roomId, bool mute, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;
            var member = await _context.ChatRoomMembers
                .FirstOrDefaultAsync(m => m.ChatRoomId == roomId && m.UserId == userId && !m.Deleted, cancellationToken);

            if (member == null)
                return false;

            member.SetMuted(mute);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<ChatInviteResultDto> InviteUsersToRoomAsync(Guid roomId, InviteUsersToChatDto dto, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            // Verify inviter is a member of the room
            if (!await IsUserMemberOfRoomAsync(roomId, cancellationToken))
                throw new UnauthorizedAccessException("You are not a member of this chat room");

            var room = await _context.ChatRooms
                .FirstOrDefaultAsync(r => r.Id == roomId && !r.Deleted, cancellationToken);

            if (room == null)
                throw new ArgumentException("Chat room not found");

            // Cannot invite to global chat (users join automatically)
            if (room.Type == ChatRoomType.Global)
                throw new InvalidOperationException("Cannot invite users to global chat");

            var newMembers = new List<ChatMemberDto>();
            var now = DateTime.UtcNow;

            foreach (var inviteUserId in dto.UserIds)
            {
                // Skip if already a member
                var isMember = await _context.ChatRoomMembers
                    .AnyAsync(m => m.ChatRoomId == roomId && m.UserId == inviteUserId && !m.Deleted, cancellationToken);

                if (isMember)
                    continue;

                // Verify user exists
                var user = await _context.Users.FindAsync(new object[] { inviteUserId }, cancellationToken);
                if (user == null)
                    continue;

                var member = new ChatRoomMember(Guid.NewGuid(), roomId, inviteUserId);
                if (!dto.IncludeHistory)
                {
                    member.SetHistoryVisibleFrom(now);
                }

                _context.ChatRoomMembers.Add(member);
                newMembers.Add(await MapToMemberDtoAsync(member, cancellationToken));
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new ChatInviteResultDto
            {
                Room = await GetChatRoomAsync(roomId, cancellationToken),
                NewMembers = newMembers
            };
        }

        public async Task<List<ChatMemberDto>> GetRoomMembersAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            return await GetRoomMembersInternalAsync(roomId, cancellationToken);
        }

        #endregion

        #region Message Operations

        public async Task<ChatMessageDto> SendMessageAsync(Guid roomId, SendMessageDto messageDto, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            // Verify sender is member of the room
            if (!await IsUserMemberOfRoomAsync(roomId, cancellationToken))
                throw new UnauthorizedAccessException("User is not a member of this chat room");

            var message = new ChatMessage(
                Guid.NewGuid(),
                roomId,
                userId,
                messageDto.Content,
                messageDto.ReplyToMessageId);

            _context.ChatMessages.Add(message);

            // Update room's last message timestamp
            var room = await _context.ChatRooms.FindAsync(new object[] { roomId }, cancellationToken);
            if (room != null)
            {
                room.UpdateLastMessageAt(message.SentAt);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return await MapToMessageDtoAsync(message, cancellationToken);
        }

        public async Task<ChatMessageDto> SendMessageWithAttachmentsAsync(
            Guid roomId,
            string content,
            Guid? replyToMessageId,
            List<(string fileName, string contentType, byte[] data)> files,
            CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            // Verify sender is member of the room
            if (!await IsUserMemberOfRoomAsync(roomId, cancellationToken))
                throw new UnauthorizedAccessException("User is not a member of this chat room");

            // Create the message (content can be empty if only attachments)
            var message = new ChatMessage(
                Guid.NewGuid(),
                roomId,
                userId,
                content ?? string.Empty,
                replyToMessageId);

            _context.ChatMessages.Add(message);

            // Save attachments
            foreach (var (fileName, contentType, data) in files)
            {
                try
                {
                    // Save file to storage
                    var storagePath = await _fileAccessor.SaveAsync(roomId, fileName, contentType, data);

                    // Create attachment entity
                    var attachment = new ChatMessageAttachment(
                        Guid.NewGuid(),
                        message.Id,
                        fileName,
                        storagePath,
                        contentType,
                        data.Length);

                    _context.ChatMessageAttachments.Add(attachment);
                }
                catch (ArgumentException ex)
                {
                    _logger.LogWarning(ex, "Failed to save attachment {FileName}: {Message}", fileName, ex.Message);
                    // Continue with other files even if one fails
                }
            }

            // Update room's last message timestamp
            var room = await _context.ChatRooms.FindAsync(new object[] { roomId }, cancellationToken);
            if (room != null)
            {
                room.UpdateLastMessageAt(message.SentAt);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return await MapToMessageDtoAsync(message, cancellationToken);
        }

        public async Task<List<ChatMessageDto>> GetMessagesAsync(Guid roomId, int pageSize = 50, DateTime? before = null, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            // Get membership to check history visibility
            var membership = await _context.ChatRoomMembers
                .FirstOrDefaultAsync(m => m.ChatRoomId == roomId && m.UserId == userId && !m.Deleted, cancellationToken);

            if (membership == null)
                throw new UnauthorizedAccessException("User is not a member of this chat room");

            var query = _context.ChatMessages
                .Where(m => m.ChatRoomId == roomId && !m.Deleted);

            // Filter messages based on HistoryVisibleFrom (if user was invited without history)
            if (membership.HistoryVisibleFrom.HasValue)
            {
                query = query.Where(m => m.SentAt >= membership.HistoryVisibleFrom.Value);
            }

            if (before.HasValue)
            {
                query = query.Where(m => m.SentAt < before.Value);
            }

            var messages = await query
                .OrderByDescending(m => m.SentAt)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var messageDtos = new List<ChatMessageDto>();
            foreach (var message in messages.AsEnumerable().Reverse()) // Return in chronological order
            {
                messageDtos.Add(await MapToMessageDtoAsync(message, cancellationToken));
            }

            return messageDtos;
        }

        public async Task<ChatMessageDto> GetMessageAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            var message = await _context.ChatMessages
                .FirstOrDefaultAsync(m => m.Id == messageId && !m.Deleted, cancellationToken);

            return message != null ? await MapToMessageDtoAsync(message, cancellationToken) : null;
        }

        public async Task<bool> DeleteMessageAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;
            var message = await _context.ChatMessages
                .FirstOrDefaultAsync(m => m.Id == messageId && !m.Deleted, cancellationToken);

            if (message == null)
                return false;

            // Only sender can delete their own message
            if (message.SenderId != userId)
                return false;

            message.MarkAsDeleted();
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ChatMessageDto> EditMessageAsync(Guid messageId, string newContent, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;
            var message = await _context.ChatMessages
                .FirstOrDefaultAsync(m => m.Id == messageId && !m.Deleted, cancellationToken);

            if (message == null)
                return null;

            // Only sender can edit their own message
            if (message.SenderId != userId)
                return null;

            // Check if message is within the edit window (15 minutes)
            if (DateTime.UtcNow - message.SentAt > EditWindowDuration)
                return null;

            // Cannot edit deleted messages
            if (message.IsDeleted)
                return null;

            message.UpdateContent(newContent);
            await _context.SaveChangesAsync(cancellationToken);

            return await MapToMessageDtoAsync(message, cancellationToken);
        }

        #endregion

        #region Read Status

        public async Task MarkRoomAsReadAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;
            var member = await _context.ChatRoomMembers
                .FirstOrDefaultAsync(m => m.ChatRoomId == roomId && m.UserId == userId && !m.Deleted, cancellationToken);

            if (member != null)
            {
                member.UpdateLastReadAt(DateTime.UtcNow);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<int> GetUnreadCountAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;
            return await GetUnreadCountForRoomAsync(roomId, userId, cancellationToken);
        }

        public async Task<int> GetTotalUnreadCountAsync(CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            var memberships = await _context.ChatRoomMembers
                .Where(m => m.UserId == userId && !m.Deleted)
                .ToListAsync(cancellationToken);

            var totalUnread = 0;
            foreach (var membership in memberships)
            {
                totalUnread += await GetUnreadCountForRoomAsync(membership.ChatRoomId, userId, cancellationToken);
            }

            return totalUnread;
        }

        #endregion

        #region Reactions

        public async Task<(bool added, List<MessageReactionDto> reactions)> ToggleReactionAsync(Guid messageId, string emoji, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            var message = await _context.ChatMessages
                .FirstOrDefaultAsync(m => m.Id == messageId && !m.Deleted, cancellationToken);

            if (message == null)
                throw new ArgumentException("Message not found");

            // Check if user is member of the room
            if (!await IsUserMemberOfRoomAsync(message.ChatRoomId, cancellationToken))
                throw new UnauthorizedAccessException("User is not a member of this chat room");

            // Check if reaction already exists
            var existingReaction = await _context.MessageReactions
                .FirstOrDefaultAsync(r => r.MessageId == messageId && r.UserId == userId && r.Emoji == emoji && !r.Deleted, cancellationToken);

            bool added;
            if (existingReaction != null)
            {
                // Remove existing reaction
                _context.Remove(existingReaction);
                added = false;
            }
            else
            {
                // Add new reaction
                var reaction = new MessageReaction(Guid.NewGuid(), messageId, userId, emoji);
                _context.MessageReactions.Add(reaction);
                added = true;
            }

            await _context.SaveChangesAsync(cancellationToken);

            // Get updated reactions
            var reactions = await GetReactionsAsync(messageId, cancellationToken);
            return (added, reactions);
        }

        public async Task<List<MessageReactionDto>> GetReactionsAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            var reactions = await _context.MessageReactions
                .Where(r => r.MessageId == messageId && !r.Deleted)
                .ToListAsync(cancellationToken);

            // Group by emoji
            var grouped = reactions
                .GroupBy(r => r.Emoji)
                .Select(g => new MessageReactionDto
                {
                    Emoji = g.Key,
                    Count = g.Count(),
                    UserIds = g.Select(r => r.UserId).ToList(),
                    UserNames = g.Select(r => GetUserDisplayName(r.User)).ToList(),
                    HasReacted = g.Any(r => r.UserId == userId)
                })
                .OrderByDescending(r => r.Count)
                .ToList();

            return grouped;
        }

        #endregion

        #region Attachments

        public async Task<ChatAttachmentDto> UploadAttachmentAsync(Guid roomId, Guid messageId, string fileName, string contentType, byte[] fileData, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            // Verify user is member of the room
            if (!await IsUserMemberOfRoomAsync(roomId, cancellationToken))
                throw new UnauthorizedAccessException("User is not a member of this chat room");

            // Verify message belongs to this room
            var message = await _context.ChatMessages
                .FirstOrDefaultAsync(m => m.Id == messageId && m.ChatRoomId == roomId && !m.Deleted, cancellationToken);

            if (message == null)
                throw new ArgumentException("Message not found in this room");

            // Save file to storage
            var storagePath = await _fileAccessor.SaveAsync(roomId, fileName, contentType, fileData);

            // Create attachment entity
            var attachment = new ChatMessageAttachment(
                Guid.NewGuid(),
                messageId,
                fileName,
                storagePath,
                contentType,
                fileData.Length);

            _context.ChatMessageAttachments.Add(attachment);
            await _context.SaveChangesAsync(cancellationToken);

            return new ChatAttachmentDto
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                ContentType = attachment.ContentType,
                FileSize = attachment.FileSize,
                DownloadUrl = $"/api/chat/attachments/{attachment.Id}/download",
                ThumbnailUrl = null,
                IsImage = attachment.IsImage,
                IsVideo = attachment.IsVideo,
                IsAudio = attachment.IsAudio,
                IsPdf = attachment.IsPdf
            };
        }

        public async Task<string> GetAttachmentDownloadUrlAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var attachment = await _context.ChatMessageAttachments
                .FirstOrDefaultAsync(a => a.Id == attachmentId && !a.Deleted, cancellationToken);

            if (attachment == null)
                return null;

            return _fileAccessor.GetDownloadUrl(attachment.StoragePath);
        }

        public async Task<(byte[] content, string fileName, string contentType)?> GetAttachmentFileAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var userId = _userAccessor.UserId;

            var attachment = await _context.ChatMessageAttachments
                .Include(a => a.ChatMessage)
                .FirstOrDefaultAsync(a => a.Id == attachmentId && !a.Deleted, cancellationToken);

            if (attachment == null)
                return null;

            // Verify user is member of the room
            if (!await IsUserMemberOfRoomAsync(attachment.ChatMessage.ChatRoomId, cancellationToken))
                return null;

            var fileResult = await _fileAccessor.GetAsync(attachment.StoragePath);
            if (fileResult == null)
                return null;

            return (fileResult.Content, attachment.FileName, attachment.ContentType);
        }

        #endregion

        #region Helper Methods

        private async Task<int> GetUnreadCountForRoomAsync(Guid roomId, Guid userId, CancellationToken cancellationToken)
        {
            var member = await _context.ChatRoomMembers
                .FirstOrDefaultAsync(m => m.ChatRoomId == roomId && m.UserId == userId && !m.Deleted, cancellationToken);

            if (member == null)
                return 0;

            var query = _context.ChatMessages
                .Where(m => m.ChatRoomId == roomId && !m.IsDeleted && !m.Deleted && m.SenderId != userId);

            if (member.LastReadAt.HasValue)
            {
                query = query.Where(m => m.SentAt > member.LastReadAt.Value);
            }

            return await query.CountAsync(cancellationToken);
        }

        private async Task<ChatMessageDto> GetLastMessageAsync(Guid roomId, CancellationToken cancellationToken)
        {
            var message = await _context.ChatMessages
                .Where(m => m.ChatRoomId == roomId && !m.IsDeleted && !m.Deleted)
                .OrderByDescending(m => m.SentAt)
                .FirstOrDefaultAsync(cancellationToken);

            return message != null ? await MapToMessageDtoAsync(message, cancellationToken) : null;
        }

        private async Task<string> GetRoomDisplayNameAsync(ChatRoom room, Guid currentUserId, CancellationToken cancellationToken)
        {
            if (room.Type == ChatRoomType.Global)
                return "Global Chat";

            if (room.Type == ChatRoomType.Project)
                return room.Project?.Title ?? room.Name ?? "Project Chat";

            if (room.Type == ChatRoomType.Direct)
            {
                // For direct chats, show the other person's name
                var otherMember = await _context.ChatRoomMembers
                    .FirstOrDefaultAsync(m => m.ChatRoomId == room.Id && m.UserId != currentUserId && !m.Deleted, cancellationToken);

                if (otherMember?.User?.Person != null)
                {
                    return otherMember.User.Person.DisplayName;
                }
            }

            return room.Name ?? "Chat";
        }

        private async Task<List<ChatMemberDto>> GetRoomMembersInternalAsync(Guid roomId, CancellationToken cancellationToken)
        {
            var members = await _context.ChatRoomMembers
                .Where(m => m.ChatRoomId == roomId && !m.Deleted)
                .ToListAsync(cancellationToken);

            var memberDtos = new List<ChatMemberDto>();
            foreach (var member in members)
            {
                memberDtos.Add(await MapToMemberDtoAsync(member, cancellationToken));
            }

            return memberDtos;
        }

        private async Task<ChatMemberDto> MapToMemberDtoAsync(ChatRoomMember member, CancellationToken cancellationToken)
        {
            var user = member.User ?? await _context.Users.FindAsync(new object[] { member.UserId }, cancellationToken);
            var onlineUsers = await _presenceTracker.GetOnlineUsers();

            return new ChatMemberDto
            {
                UserId = member.UserId,
                PersonId = user?.PersonId ?? Guid.Empty,
                DisplayName = GetUserDisplayName(user),
                AvatarUrl = null, // TODO: Add avatar support
                Instrument = GetUserMainInstrument(user),
                JoinedAt = member.JoinedAt,
                IsMuted = member.IsMuted,
                IsOnline = onlineUsers.Any(u => u.UserId == member.UserId)
            };
        }

        private async Task<ChatMessageDto> MapToMessageDtoAsync(ChatMessage message, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.UserId;
            var sender = message.Sender ?? await _context.Users.FindAsync(new object[] { message.SenderId }, cancellationToken);
            var isOwnMessage = message.SenderId == userId;
            var isWithinEditWindow = DateTime.UtcNow - message.SentAt < EditWindowDuration;

            // Get attachments
            var attachments = await _context.ChatMessageAttachments
                .Where(a => a.ChatMessageId == message.Id && !a.Deleted)
                .ToListAsync(cancellationToken);

            // Get reactions
            var reactions = await GetReactionsAsync(message.Id, cancellationToken);

            // Get reply-to message if exists
            ChatMessageDto replyToMessage = null;
            if (message.ReplyToMessageId.HasValue)
            {
                var replyTo = await _context.ChatMessages
                    .FirstOrDefaultAsync(m => m.Id == message.ReplyToMessageId && !m.Deleted, cancellationToken);
                if (replyTo != null)
                {
                    var replyToSender = replyTo.Sender ?? await _context.Users.FindAsync(new object[] { replyTo.SenderId }, cancellationToken);
                    replyToMessage = new ChatMessageDto
                    {
                        Id = replyTo.Id,
                        ChatRoomId = replyTo.ChatRoomId,
                        SenderId = replyTo.SenderId,
                        SenderName = GetUserDisplayName(replyToSender),
                        Content = replyTo.IsDeleted ? "[Nachricht gelöscht]" : replyTo.Content,
                        SentAt = replyTo.SentAt,
                        IsDeleted = replyTo.IsDeleted
                    };
                }
            }

            return new ChatMessageDto
            {
                Id = message.Id,
                ChatRoomId = message.ChatRoomId,
                SenderId = message.SenderId,
                SenderName = GetUserDisplayName(sender),
                SenderAvatarUrl = null, // TODO: Add avatar support
                Content = message.IsDeleted ? "[Nachricht gelöscht]" : message.Content,
                SentAt = message.SentAt,
                EditedAt = message.EditedAt,
                IsDeleted = message.IsDeleted,
                ReplyToMessageId = message.ReplyToMessageId,
                ReplyToMessage = replyToMessage,
                IsOwnMessage = isOwnMessage,
                CanEdit = isOwnMessage && isWithinEditWindow && !message.IsDeleted,
                CanDelete = isOwnMessage && !message.IsDeleted,
                Attachments = attachments.Select(a => new ChatAttachmentDto
                {
                    Id = a.Id,
                    FileName = a.FileName,
                    ContentType = a.ContentType,
                    FileSize = a.FileSize,
                    DownloadUrl = $"/api/chat/attachments/{a.Id}/download",
                    ThumbnailUrl = a.ThumbnailPath != null ? $"/api/chat/attachments/{a.Id}/thumbnail" : null,
                    IsImage = a.IsImage,
                    IsVideo = a.IsVideo,
                    IsAudio = a.IsAudio,
                    IsPdf = a.IsPdf
                }).ToList(),
                Reactions = reactions
            };
        }

        private static string GetUserDisplayName(User user)
        {
            if (user?.Person != null)
            {
                return user.Person.DisplayName;
            }
            return user?.UserName ?? "Unbekannt";
        }

        private static string GetUserMainInstrument(User user)
        {
            // Get the main instrument from the user's first musician profile
            var section = user?.Person?.MusicianProfiles?.FirstOrDefault()?.Instrument;
            return section?.Name;
        }

        #endregion
    }
}
