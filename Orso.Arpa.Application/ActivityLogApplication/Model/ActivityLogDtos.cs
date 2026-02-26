using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.ActivityLogApplication.Model;

public class ActivityLogDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Action { get; set; }
    public string Category { get; set; }
    public string EntityType { get; set; }
    public Guid? EntityId { get; set; }
    public string EntityLabel { get; set; }
    public string Path { get; set; }
    public string Metadata { get; set; }
}

public class ActivityLogFilterDto
{
    public Guid? UserId { get; set; }
    public string Action { get; set; }
    public string Category { get; set; }
    public string EntityType { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string Search { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

public class ActivityLogResultDto
{
    public List<ActivityLogDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}

public class ActivityLogStatsDto
{
    public int TotalEntries { get; set; }
    public int TotalUsers { get; set; }
    public int TotalCategories { get; set; }
    public DateTime? OldestEntry { get; set; }
    public DateTime? NewestEntry { get; set; }
    public List<ActivityLogCategoryStatsDto> TopCategories { get; set; } = new();
    public List<ActivityLogActionStatsDto> TopActions { get; set; } = new();
}

public class ActivityLogCategoryStatsDto
{
    public string Category { get; set; }
    public int Count { get; set; }
}

public class ActivityLogActionStatsDto
{
    public string Action { get; set; }
    public int Count { get; set; }
}

public class ActivityLogUserDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public int EventCount { get; set; }
    public DateTime? LastActivity { get; set; }
}

public class CreateActivityLogDto
{
    public string Action { get; set; }
    public string Category { get; set; }
    public string EntityType { get; set; }
    public Guid? EntityId { get; set; }
    public string EntityLabel { get; set; }
    public string Path { get; set; }
    public string Metadata { get; set; }
    public DateTime? Timestamp { get; set; }
}

public class CreateActivityLogBatchDto
{
    public List<CreateActivityLogDto> Events { get; set; } = new();
}
