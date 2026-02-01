using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.ChatApplication.Model
{
    public enum ChatRoomTypeDto
    {
        Direct = 0,
        Project = 1,
        Global = 2
    }

    public class ChatRoomDto
    {
        public Guid Id { get; set; }
        public ChatRoomTypeDto Type { get; set; }
        public string Name { get; set; }
        public Guid? ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastMessageAt { get; set; }
        public ChatMessageDto LastMessage { get; set; }
        public int UnreadCount { get; set; }
        public List<ChatMemberDto> Members { get; set; } = new();
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

    public class CreateDirectChatDto
    {
        public Guid OtherPersonId { get; set; }
    }

    public class CreateProjectChatDto
    {
        public Guid ProjectId { get; set; }
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
}
