using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.AnnouncementApplication.Interfaces;
using Orso.Arpa.Application.AnnouncementApplication.Model;
using Orso.Arpa.Domain.AnnouncementDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Interfaces;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence.DataAccess;

namespace Orso.Arpa.Application.AnnouncementApplication.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly ArpaContext _context;
    private readonly ITokenAccessor _tokenAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IRealtimeNotificationSender _notificationSender;

    public AnnouncementService(
        ArpaContext context,
        ITokenAccessor tokenAccessor,
        IDateTimeProvider dateTimeProvider,
        IRealtimeNotificationSender notificationSender)
    {
        _context = context;
        _tokenAccessor = tokenAccessor;
        _dateTimeProvider = dateTimeProvider;
        _notificationSender = notificationSender;
    }

    public async Task<UnreadAnnouncementsDto> GetUnreadAsync()
    {
        var userId = _tokenAccessor.UserId;
        var now = _dateTimeProvider.GetUtcNow();

        var announcements = await _context.Announcements
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(a => !a.Deleted && a.Active && (a.ValidUntil == null || a.ValidUntil > now))
            .OrderByDescending(a => a.SortOrder)
            .ThenByDescending(a => a.CreatedAt)
            .Take(20)
            .ToListAsync();

        var reads = await _context.AnnouncementReads
            .AsNoTracking()
            .Where(r => r.UserId == userId && announcements.Select(a => a.Id).Contains(r.AnnouncementId))
            .ToListAsync();

        var readIds = reads.Select(r => r.AnnouncementId).ToHashSet();
        var unread = announcements.Where(a => !readIds.Contains(a.Id)).ToList();

        // Load last 5 recently read announcements
        var recentlyRead = announcements
            .Where(a => readIds.Contains(a.Id))
            .Select(a => new { Announcement = a, ReadAt = reads.First(r => r.AnnouncementId == a.Id).ReadAt })
            .OrderByDescending(x => x.ReadAt)
            .Take(5)
            .Select(x => MapToDto(x.Announcement))
            .ToList();

        return new UnreadAnnouncementsDto
        {
            Items = unread.Take(10).Select(MapToDto).ToList(),
            TotalCount = unread.Count,
            RecentlyRead = recentlyRead,
        };
    }

    public async Task<UserAnnouncementsResponseDto> GetAllForUserAsync()
    {
        var userId = _tokenAccessor.UserId;
        var now = _dateTimeProvider.GetUtcNow();

        var announcements = await _context.Announcements
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(a => !a.Deleted && a.Active && (a.ValidUntil == null || a.ValidUntil > now))
            .OrderByDescending(a => a.SortOrder)
            .ThenByDescending(a => a.CreatedAt)
            .ToListAsync();

        var reads = await _context.AnnouncementReads
            .AsNoTracking()
            .Where(r => r.UserId == userId && announcements.Select(a => a.Id).Contains(r.AnnouncementId))
            .ToListAsync();

        var readLookup = reads.ToDictionary(r => r.AnnouncementId);

        var items = announcements.Select(a =>
        {
            readLookup.TryGetValue(a.Id, out var read);
            return new UserAnnouncementDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                Priority = a.Priority,
                Link = a.Link,
                LinkText = a.LinkText,
                Active = a.Active,
                ValidUntil = a.ValidUntil,
                SortOrder = a.SortOrder,
                CreatedAt = a.CreatedAt,
                CreatedBy = a.CreatedBy,
                IsRead = read?.ReadAt != null,
                ReadAt = read?.ReadAt,
                TickerPinned = read?.TickerPinned ?? false,
            };
        }).ToList();

        return new UserAnnouncementsResponseDto
        {
            Items = items,
            UnreadCount = items.Count(i => !i.IsRead),
        };
    }

    public async Task<List<AnnouncementDto>> GetTickerItemsAsync()
    {
        var userId = _tokenAccessor.UserId;
        var now = _dateTimeProvider.GetUtcNow();

        var pinnedAnnouncementIds = await _context.AnnouncementReads
            .AsNoTracking()
            .Where(r => r.UserId == userId && r.TickerPinned)
            .Select(r => r.AnnouncementId)
            .ToListAsync();

        if (pinnedAnnouncementIds.Count == 0)
            return new List<AnnouncementDto>();

        var announcements = await _context.Announcements
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(a => !a.Deleted && a.Active && pinnedAnnouncementIds.Contains(a.Id) && (a.ValidUntil == null || a.ValidUntil > now))
            .OrderByDescending(a => a.SortOrder)
            .ThenByDescending(a => a.CreatedAt)
            .ToListAsync();

        return announcements.Select(MapToDto).ToList();
    }

    public async Task<List<AnnouncementAdminDto>> GetAllAsync()
    {
        var announcements = await _context.Announcements
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(a => !a.Deleted)
            .OrderByDescending(a => a.Active)
            .ThenByDescending(a => a.SortOrder)
            .ThenByDescending(a => a.CreatedAt)
            .ToListAsync();

        var totalUsers = await _context.Users.CountAsync();

        var readInfos = await _context.AnnouncementReads
            .AsNoTracking()
            .Where(r => announcements.Select(a => a.Id).Contains(r.AnnouncementId))
            .Select(r => new { r.AnnouncementId, r.UserId, r.ReadAt, r.TickerPinned, r.User.UserName })
            .ToListAsync();

        return announcements.Select(a =>
        {
            var reads = readInfos.Where(r => r.AnnouncementId == a.Id).ToList();
            return new AnnouncementAdminDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                Priority = a.Priority,
                Link = a.Link,
                LinkText = a.LinkText,
                Active = a.Active,
                ValidUntil = a.ValidUntil,
                SortOrder = a.SortOrder,
                CreatedAt = a.CreatedAt,
                CreatedBy = a.CreatedBy,
                ReadCount = reads.Count,
                TotalUserCount = totalUsers,
                ReadDetails = reads.Select(r => new AnnouncementReadInfoDto
                {
                    UserId = r.UserId,
                    DisplayName = r.UserName,
                    ReadAt = r.ReadAt,
                    TickerPinned = r.TickerPinned,
                }).ToList(),
            };
        }).ToList();
    }

    public async Task<AnnouncementDto> CreateAsync(CreateAnnouncementDto dto)
    {
        var announcement = new Announcement(
            null, dto.Title, dto.Content, dto.Priority ?? "info",
            dto.Link, dto.LinkText, true, dto.ValidUntil, dto.SortOrder);

        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        await _notificationSender.SendDashboardUpdateToAllAsync("announcement", announcement.Id);

        return MapToDto(announcement);
    }

    public async Task UpdateAsync(UpdateAnnouncementDto dto)
    {
        var announcement = await _context.Announcements
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(a => a.Id == dto.Id && !a.Deleted)
            ?? throw new KeyNotFoundException($"Announcement {dto.Id} not found");

        announcement.Update(dto.Title, dto.Content, dto.Priority, dto.Link, dto.LinkText, dto.ValidUntil, dto.SortOrder);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var announcement = await _context.Announcements
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(a => a.Id == id && !a.Deleted)
            ?? throw new KeyNotFoundException($"Announcement {id} not found");

        _context.Announcements.Remove(announcement);
        await _context.SaveChangesAsync();
    }

    public async Task ToggleActiveAsync(Guid id)
    {
        var announcement = await _context.Announcements
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(a => a.Id == id && !a.Deleted)
            ?? throw new KeyNotFoundException($"Announcement {id} not found");

        announcement.ToggleActive();
        await _context.SaveChangesAsync();
    }

    public async Task MarkAsReadAsync(Guid id)
    {
        var userId = _tokenAccessor.UserId;
        var existing = await _context.AnnouncementReads
            .FirstOrDefaultAsync(r => r.AnnouncementId == id && r.UserId == userId);

        if (existing != null)
            return;

        _context.AnnouncementReads.Add(new AnnouncementRead
        {
            Id = Guid.NewGuid(),
            AnnouncementId = id,
            UserId = userId,
            ReadAt = _dateTimeProvider.GetUtcNow(),
            TickerPinned = false,
        });
        await _context.SaveChangesAsync();
    }

    public async Task MarkAllAsReadAsync()
    {
        var userId = _tokenAccessor.UserId;
        var now = _dateTimeProvider.GetUtcNow();

        var unreadIds = await _context.Announcements
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(a => !a.Deleted && a.Active && (a.ValidUntil == null || a.ValidUntil > now))
            .Select(a => a.Id)
            .ToListAsync();

        var alreadyReadIds = await _context.AnnouncementReads
            .AsNoTracking()
            .Where(r => r.UserId == userId && unreadIds.Contains(r.AnnouncementId))
            .Select(r => r.AnnouncementId)
            .ToListAsync();

        var toMark = unreadIds.Except(alreadyReadIds).ToList();

        foreach (var announcementId in toMark)
        {
            _context.AnnouncementReads.Add(new AnnouncementRead
            {
                Id = Guid.NewGuid(),
                AnnouncementId = announcementId,
                UserId = userId,
                ReadAt = now,
                TickerPinned = false,
            });
        }

        if (toMark.Count > 0)
            await _context.SaveChangesAsync();
    }

    public async Task ToggleTickerPinAsync(Guid id)
    {
        var userId = _tokenAccessor.UserId;
        var existing = await _context.AnnouncementReads
            .FirstOrDefaultAsync(r => r.AnnouncementId == id && r.UserId == userId);

        if (existing == null)
        {
            // Mark as read + pinned
            _context.AnnouncementReads.Add(new AnnouncementRead
            {
                Id = Guid.NewGuid(),
                AnnouncementId = id,
                UserId = userId,
                ReadAt = _dateTimeProvider.GetUtcNow(),
                TickerPinned = true,
            });
        }
        else
        {
            existing.TickerPinned = !existing.TickerPinned;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<AnnouncementDto> CreateDeployStartingAnnouncementAsync(DeployAnnouncementDto dto)
    {
        var component = string.IsNullOrEmpty(dto.Component) ? "ARPA" : dto.Component;
        var title = $"Wartung: {component} wird aktualisiert";
        var content = "Das System wird in wenigen Minuten aktualisiert. Bitte speichere deine Arbeit. Wir sind gleich zurück!";
        var now = _dateTimeProvider.GetUtcNow();

        // Deactivate old maintenance announcements (duplicate protection)
        var oldMaintenanceAnnouncements = await _context.Announcements
            .IgnoreQueryFilters()
            .Where(a => !a.Deleted && a.Active && a.Title.StartsWith("Wartung:"))
            .ToListAsync();

        foreach (var old in oldMaintenanceAnnouncements)
        {
            old.Deactivate();
        }

        var announcement = new Announcement(
            null, title, content, "warning",
            null, null, true, now.AddMinutes(30), 200);

        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        // Pin for all active users
        var userIds = await _context.Users
            .AsNoTracking()
            .Where(u => u.EmailConfirmed)
            .Select(u => u.Id)
            .ToListAsync();

        foreach (var userId in userIds)
        {
            _context.AnnouncementReads.Add(new AnnouncementRead
            {
                Id = Guid.NewGuid(),
                AnnouncementId = announcement.Id,
                UserId = userId,
                ReadAt = now,
                TickerPinned = true,
            });
        }

        await _context.SaveChangesAsync();

        await _notificationSender.SendDashboardUpdateToAllAsync("announcement", announcement.Id);

        return MapToDto(announcement);
    }

    public async Task<AnnouncementDto> CreateDeployAnnouncementAsync(DeployAnnouncementDto dto)
    {
        // Deactivate maintenance warnings (deploy is done)
        var maintenanceAnnouncements = await _context.Announcements
            .IgnoreQueryFilters()
            .Where(a => !a.Deleted && a.Active && a.Title.StartsWith("Wartung:"))
            .ToListAsync();

        foreach (var maintenance in maintenanceAnnouncements)
        {
            maintenance.Deactivate();
        }

        var component = string.IsNullOrEmpty(dto.Component) ? "ARPA" : dto.Component;
        var title = $"{component} v{dto.Version}";
        var content = !string.IsNullOrWhiteSpace(dto.Changelog)
            ? dto.Changelog
            : $"Version {dto.Version} wurde erfolgreich bereitgestellt.";

        var announcement = new Announcement(
            null, title, content, "feature",
            "/user/system-info", "Changelog", true, null, 100);

        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        // No auto-pin: the announcement appears as unread in the bell for all users
        await _notificationSender.SendDashboardUpdateToAllAsync("announcement", announcement.Id);

        return MapToDto(announcement);
    }

    private static AnnouncementDto MapToDto(Announcement a) => new()
    {
        Id = a.Id,
        Title = a.Title,
        Content = a.Content,
        Priority = a.Priority,
        Link = a.Link,
        LinkText = a.LinkText,
        Active = a.Active,
        ValidUntil = a.ValidUntil,
        SortOrder = a.SortOrder,
        CreatedAt = a.CreatedAt,
        CreatedBy = a.CreatedBy,
    };
}
