using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.ActivityLogApplication.Interfaces;
using Orso.Arpa.Application.ActivityLogApplication.Model;
using Orso.Arpa.Domain.ActivityLogDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Application.ActivityLogApplication.Services;

public class ActivityLogService : IActivityLogService
{
    private readonly IArpaContext _context;

    public ActivityLogService(IArpaContext context)
    {
        _context = context;
    }

    public async Task<ActivityLogResultDto> GetLogsAsync(ActivityLogFilterDto filter, CancellationToken ct = default)
    {
        var query = _context.ActivityLogs.AsNoTracking().AsQueryable();

        if (filter.UserId.HasValue)
            query = query.Where(l => l.UserId == filter.UserId.Value);

        if (!string.IsNullOrWhiteSpace(filter.Action))
            query = query.Where(l => l.Action == filter.Action);

        if (!string.IsNullOrWhiteSpace(filter.Category))
            query = query.Where(l => l.Category == filter.Category);

        if (!string.IsNullOrWhiteSpace(filter.EntityType))
            query = query.Where(l => l.EntityType == filter.EntityType);

        if (filter.From.HasValue)
            query = query.Where(l => l.CreatedAt >= filter.From.Value);

        if (filter.To.HasValue)
            query = query.Where(l => l.CreatedAt <= filter.To.Value);

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.ToLower();
            query = query.Where(l =>
                l.Username.ToLower().Contains(search) ||
                (l.EntityLabel != null && l.EntityLabel.ToLower().Contains(search)) ||
                (l.Path != null && l.Path.ToLower().Contains(search)));
        }

        int totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(l => l.CreatedAt)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(l => new ActivityLogDto
            {
                Id = l.Id,
                CreatedAt = l.CreatedAt,
                UserId = l.UserId,
                Username = l.Username,
                Action = l.Action,
                Category = l.Category,
                EntityType = l.EntityType,
                EntityId = l.EntityId,
                EntityLabel = l.EntityLabel,
                Path = l.Path,
                Metadata = l.Metadata
            })
            .ToListAsync(ct);

        return new ActivityLogResultDto
        {
            Items = items,
            TotalCount = totalCount,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize)
        };
    }

    public async Task<ActivityLogStatsDto> GetStatsAsync(CancellationToken ct = default)
    {
        var query = _context.ActivityLogs.AsNoTracking();

        var stats = new ActivityLogStatsDto
        {
            TotalEntries = await query.CountAsync(ct),
            TotalUsers = await query.Select(l => l.UserId).Distinct().CountAsync(ct),
            TotalCategories = await query.Select(l => l.Category).Distinct().CountAsync(ct),
            OldestEntry = await query.OrderBy(l => l.CreatedAt).Select(l => (DateTime?)l.CreatedAt).FirstOrDefaultAsync(ct),
            NewestEntry = await query.OrderByDescending(l => l.CreatedAt).Select(l => (DateTime?)l.CreatedAt).FirstOrDefaultAsync(ct),
        };

        stats.TopCategories = await query
            .GroupBy(l => l.Category)
            .Select(g => new ActivityLogCategoryStatsDto { Category = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToListAsync(ct);

        stats.TopActions = await query
            .GroupBy(l => l.Action)
            .Select(g => new ActivityLogActionStatsDto { Action = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToListAsync(ct);

        return stats;
    }

    public async Task<List<ActivityLogUserDto>> GetDistinctUsersAsync(CancellationToken ct = default)
    {
        return await _context.ActivityLogs
            .AsNoTracking()
            .GroupBy(l => new { l.UserId, l.Username })
            .Select(g => new ActivityLogUserDto
            {
                UserId = g.Key.UserId,
                Username = g.Key.Username,
                EventCount = g.Count(),
                LastActivity = g.Max(l => l.CreatedAt)
            })
            .OrderByDescending(u => u.LastActivity)
            .ToListAsync(ct);
    }

    public async Task CreateBatchAsync(List<CreateActivityLogDto> events, Guid userId, string username, CancellationToken ct = default)
    {
        if (events == null || events.Count == 0)
            return;

        var logs = events.Take(50).Select(e => new ActivityLog
        {
            UserId = userId,
            Username = username,
            Action = e.Action ?? "unknown",
            Category = e.Category ?? "unknown",
            EntityType = e.EntityType,
            EntityId = e.EntityId,
            EntityLabel = e.EntityLabel,
            Path = e.Path,
            Metadata = e.Metadata,
            CreatedAt = e.Timestamp ?? DateTime.UtcNow
        }).ToList();

        await _context.Set<ActivityLog>().AddRangeAsync(logs, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task CleanupOldLogsAsync(int retentionDays = 90, CancellationToken ct = default)
    {
        var cutoff = DateTime.UtcNow.AddDays(-retentionDays);
        await _context.ExecuteSqlAsync($"DELETE FROM activity_logs WHERE created_at < '{cutoff:yyyy-MM-dd HH:mm:ss}'");
    }
}
