using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.ChatApplication.Model
{
    public enum ChatRoomTypeDto
    {
        Direct = 0,
        Project = 1,
        Global = 2,
        Entity = 4,
        Group = 5
    }

    public class ChatRoomDto
    {
        public Guid Id { get; set; }
        public ChatRoomTypeDto Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastMessageAt { get; set; }
        public ChatMessageDto LastMessage { get; set; }
        public int UnreadCount { get; set; }
        public List<ChatMemberDto> Members { get; set; } = new();

        // Entity link (for entity-bound chats)
        public string LinkedEntityType { get; set; }
        public Guid? LinkedEntityId { get; set; }
        public string LinkedEntityDisplayName { get; set; }
    }

    public class ChatMemberDto
    {
        public Guid UserId { get; set; }
        public Guid PersonId { get; set; }
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        public string Instrument { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsMuted { get; set; }
        public bool IsOnline { get; set; }
        public DateTime? LastReadAt { get; set; }
    }

    public enum ChatMessageTypeDto
    {
        Text = 0,
        Location = 1,
        LiveLocationStart = 2
    }

    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public Guid ChatRoomId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderAvatarUrl { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ReplyToMessageId { get; set; }
        public ChatMessageDto ReplyToMessage { get; set; }
        public bool IsOwnMessage { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public ChatMessageTypeDto MessageType { get; set; }
        public LocationDataDto Location { get; set; }

        // Attachments (replaces ImageUrl - supports all file types)
        public List<ChatAttachmentDto> Attachments { get; set; } = new();

        // Reactions
        public List<MessageReactionDto> Reactions { get; set; } = new();
    }

    public class ChatAttachmentDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string DownloadUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }
        public bool IsAudio { get; set; }
        public bool IsPdf { get; set; }
    }

    public class MessageReactionDto
    {
        public string Emoji { get; set; }
        public int Count { get; set; }
        public List<Guid> UserIds { get; set; } = new();
        public List<string> UserNames { get; set; } = new();
        public bool HasReacted { get; set; }
    }

    public class ToggleReactionDto
    {
        public string Emoji { get; set; }
    }

    public class ReactionUpdateDto
    {
        public Guid MessageId { get; set; }
        public Guid ChatRoomId { get; set; }
        public string Emoji { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool Added { get; set; }
        public List<MessageReactionDto> Reactions { get; set; } = new();
    }

    public class SendMessageDto
    {
        public string Content { get; set; }
        public Guid? ReplyToMessageId { get; set; }
    }

    public class CreateGroupChatDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> MemberPersonIds { get; set; } = new();
    }

    public class UpdateChatRoomDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateDirectChatDto
    {
        public Guid OtherPersonId { get; set; }
    }

    public class CreateProjectChatDto
    {
        public Guid ProjectId { get; set; }
    }

    public class CreateEntityChatDto
    {
        public string EntityType { get; set; }
        public Guid EntityId { get; set; }
        public string EntityDisplayName { get; set; }
        public List<Guid> MemberUserIds { get; set; } = new();
    }

    public class InviteUsersToChatDto
    {
        public List<Guid> UserIds { get; set; } = new();
        public bool IncludeHistory { get; set; } = true;
    }

    public class ChatInviteResultDto
    {
        public ChatRoomDto Room { get; set; }
        public List<ChatMemberDto> NewMembers { get; set; } = new();
    }

    public class ChatRoomListDto
    {
        public List<ChatRoomDto> Rooms { get; set; } = new();
        public int TotalUnread { get; set; }
    }

    public class TypingIndicatorDto
    {
        public Guid ChatRoomId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsTyping { get; set; }
    }

    // ===== Chat Folder DTOs =====

    public class ChatFolderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        public Guid? ParentId { get; set; }
        public int SortOrder { get; set; }
        public List<ChatFolderDto> Children { get; set; } = new();
        public List<Guid> RoomIds { get; set; } = new();
    }

    public class ChatFolderConfigDto
    {
        public List<ChatFolderDto> SystemFolders { get; set; } = new();
        public List<ChatFolderDto> PersonalFolders { get; set; } = new();
        public Dictionary<string, string> RoomToFolder { get; set; } = new();
    }

    public class CreateChatFolderDto
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class UpdateChatFolderDto
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class ReorderFoldersDto
    {
        public List<Guid> FolderIds { get; set; } = new();
    }

    public class AssignRoomToFolderDto
    {
        public Guid ChatRoomId { get; set; }
    }

    public class MigrateFoldersDto
    {
        public List<MigrateFolderItemDto> Folders { get; set; } = new();
        public Dictionary<string, string> RoomToFolder { get; set; } = new();
    }

    public class MigrateFolderItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }

    // ===== Location Sharing DTOs =====

    public class LocationDataDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Accuracy { get; set; }
        public string Label { get; set; }
    }

    public class LiveLocationShareDto
    {
        public Guid Id { get; set; }
        public Guid ChatRoomId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid MessageId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Accuracy { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class SendLocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Accuracy { get; set; }
        public string Label { get; set; }
    }

    public class StartLiveLocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Accuracy { get; set; }
        public int DurationMinutes { get; set; }
    }

    public class UpdateLiveLocationDto
    {
        public Guid ShareId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Accuracy { get; set; }
    }
}
