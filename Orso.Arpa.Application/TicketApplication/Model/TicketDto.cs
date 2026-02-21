using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.TicketApplication.Model
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int? AdminPriority { get; set; }
        public string Effort { get; set; }
        public int? EstimatedMinutes { get; set; }
        public int? SpentMinutes { get; set; }
        public Guid CreatorId { get; set; }
        public string CreatorDisplayName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int VoteScore { get; set; }
        public int? CurrentUserVote { get; set; }
        public int MessageCount { get; set; }
        public bool HasUnread { get; set; }
        public List<TicketAttachmentDto> Attachments { get; set; } = new();
        public List<TicketLinkDto> Links { get; set; } = new();
        public List<TicketMessageDto> Messages { get; set; } = new();
    }

    public class TicketListItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int? AdminPriority { get; set; }
        public string Effort { get; set; }
        public Guid CreatorId { get; set; }
        public string CreatorDisplayName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int VoteScore { get; set; }
        public int? CurrentUserVote { get; set; }
        public int MessageCount { get; set; }
        public bool HasUnread { get; set; }
    }

    public class TicketMessageDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public string UserDisplayName { get; set; }
        public DateTime SentAt { get; set; }
        public List<TicketAttachmentDto> Attachments { get; set; } = new();
        public List<TicketReactionDto> Reactions { get; set; } = new();
    }

    public class TicketAttachmentDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
    }

    public class TicketLinkDto
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
    }

    public class TicketReactionDto
    {
        public string Emoji { get; set; }
        public int Count { get; set; }
        public bool CurrentUserReacted { get; set; }
    }

    public class TicketStatsDto
    {
        public int Total { get; set; }
        public int Open { get; set; }
        public int InProgress { get; set; }
        public int Done { get; set; }
        public int Rejected { get; set; }
    }

    public class CreateTicketDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }

    public class UpdateTicketDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int? AdminPriority { get; set; }
        public string Effort { get; set; }
        public int? EstimatedMinutes { get; set; }
        public int? SpentMinutes { get; set; }
    }

    public class CreateTicketMessageDto
    {
        public string Content { get; set; }
    }

    public class CreateTicketLinkDto
    {
        public string Label { get; set; }
        public string Url { get; set; }
    }

    public class VoteDto
    {
        public int Value { get; set; }
    }

    public class ToggleReactionDto
    {
        public string Emoji { get; set; }
    }
}
