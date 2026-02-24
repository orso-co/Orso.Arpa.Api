using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.AnnouncementApplication.Model;

public class AnnouncementDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Priority { get; set; }
    public string Link { get; set; }
    public string LinkText { get; set; }
    public bool Active { get; set; }
    public DateTime? ValidUntil { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}

public class UnreadAnnouncementsDto
{
    public List<AnnouncementDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

public class CreateAnnouncementDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Priority { get; set; } = "info";
    public string Link { get; set; }
    public string LinkText { get; set; }
    public DateTime? ValidUntil { get; set; }
    public int SortOrder { get; set; }
}

public class UpdateAnnouncementDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Priority { get; set; }
    public string Link { get; set; }
    public string LinkText { get; set; }
    public DateTime? ValidUntil { get; set; }
    public int SortOrder { get; set; }
}

public class AnnouncementAdminDto : AnnouncementDto
{
    public int ReadCount { get; set; }
    public int TotalUserCount { get; set; }
    public List<AnnouncementReadInfoDto> ReadDetails { get; set; } = new();
}

public class AnnouncementReadInfoDto
{
    public Guid UserId { get; set; }
    public string DisplayName { get; set; }
    public DateTime ReadAt { get; set; }
    public bool TickerPinned { get; set; }
}

public class UserAnnouncementDto : AnnouncementDto
{
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
    public bool TickerPinned { get; set; }
}

public class UserAnnouncementsResponseDto
{
    public List<UserAnnouncementDto> Items { get; set; } = new();
    public int UnreadCount { get; set; }
}

public class DeployAnnouncementDto
{
    public string Version { get; set; }
    public string Secret { get; set; }
    public string Component { get; set; }
    public string Changelog { get; set; }
}
