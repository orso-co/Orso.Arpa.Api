using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.TicketApplication.Interfaces;
using Orso.Arpa.Application.TicketApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.TicketDomain.Enums;
using Orso.Arpa.Domain.TicketDomain.Interfaces;
using Orso.Arpa.Domain.TicketDomain.Model;

namespace Orso.Arpa.Application.TicketApplication.Services
{
    public class TicketService : ITicketService
    {
        private readonly IArpaContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly ITicketFileAccessor _fileAccessor;

        public TicketService(IArpaContext context, IUserAccessor userAccessor, ITicketFileAccessor fileAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
            _fileAccessor = fileAccessor;
        }

        public async Task<List<TicketListItemDto>> GetTicketsAsync(string status, string type, Guid? creatorId, string search, CancellationToken cancellationToken = default)
        {
            var currentUserId = _userAccessor.UserId;
            var query = _context.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<TicketStatus>(status, true, out var statusEnum))
                query = query.Where(t => t.Status == statusEnum);

            if (!string.IsNullOrEmpty(type) && Enum.TryParse<TicketType>(type, true, out var typeEnum))
                query = query.Where(t => t.Type == typeEnum);

            if (creatorId.HasValue)
                query = query.Where(t => t.CreatorId == creatorId.Value);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(t => t.Title.ToLower().Contains(search.ToLower()) || t.Description.ToLower().Contains(search.ToLower()));

            var tickets = await query
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TicketListItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Type = t.Type.ToString(),
                    Status = t.Status.ToString(),
                    AdminPriority = t.AdminPriority,
                    Effort = t.Effort.HasValue ? t.Effort.Value.ToString() : null,
                    CreatorId = t.CreatorId,
                    CreatorDisplayName = t.Creator.Person != null
                        ? (t.Creator.Person.GivenName + " " + t.Creator.Person.Surname).Trim()
                        : t.Creator.UserName,
                    CreatedAt = t.CreatedAt,
                    VoteScore = t.Votes.Where(v => !v.Deleted).Sum(v => v.Value),
                    CurrentUserVote = t.Votes.Where(v => !v.Deleted && v.UserId == currentUserId).Select(v => (int?)v.Value).FirstOrDefault(),
                    MessageCount = t.Messages.Count(m => !m.Deleted),
                    HasUnread = !t.ReadStatuses.Any(rs => !rs.Deleted && rs.UserId == currentUserId)
                        || t.Messages.Any(m => !m.Deleted && m.SentAt > t.ReadStatuses
                            .Where(rs => !rs.Deleted && rs.UserId == currentUserId)
                            .Select(rs => rs.LastReadAt)
                            .FirstOrDefault())
                })
                .ToListAsync(cancellationToken);

            return tickets;
        }

        public async Task<TicketDto> GetTicketByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var currentUserId = _userAccessor.UserId;

            var ticket = await _context.Tickets
                .Where(t => t.Id == id)
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Type = t.Type.ToString(),
                    Status = t.Status.ToString(),
                    AdminPriority = t.AdminPriority,
                    Effort = t.Effort.HasValue ? t.Effort.Value.ToString() : null,
                    EstimatedMinutes = t.EstimatedMinutes,
                    SpentMinutes = t.SpentMinutes,
                    CreatorId = t.CreatorId,
                    CreatorDisplayName = t.Creator.Person != null
                        ? (t.Creator.Person.GivenName + " " + t.Creator.Person.Surname).Trim()
                        : t.Creator.UserName,
                    CreatedAt = t.CreatedAt,
                    ModifiedAt = t.ModifiedAt,
                    VoteScore = t.Votes.Where(v => !v.Deleted).Sum(v => v.Value),
                    CurrentUserVote = t.Votes.Where(v => !v.Deleted && v.UserId == currentUserId).Select(v => (int?)v.Value).FirstOrDefault(),
                    MessageCount = t.Messages.Count(m => !m.Deleted),
                    HasUnread = false,
                    Attachments = t.Attachments.Where(a => !a.Deleted && a.MessageId == null).Select(a => new TicketAttachmentDto
                    {
                        Id = a.Id,
                        FileName = a.FileName,
                        ContentType = a.ContentType,
                        FileSize = a.FileSize
                    }).ToList(),
                    Links = t.Links.Where(l => !l.Deleted).Select(l => new TicketLinkDto
                    {
                        Id = l.Id,
                        Label = l.Label,
                        Url = l.Url
                    }).ToList(),
                    Messages = t.Messages.Where(m => !m.Deleted).OrderBy(m => m.SentAt).Select(m => new TicketMessageDto
                    {
                        Id = m.Id,
                        Content = m.Content,
                        UserId = m.UserId,
                        UserDisplayName = m.User.Person != null
                            ? (m.User.Person.GivenName + " " + m.User.Person.Surname).Trim()
                            : m.User.UserName,
                        SentAt = m.SentAt,
                        Attachments = m.Attachments.Where(a => !a.Deleted).Select(a => new TicketAttachmentDto
                        {
                            Id = a.Id,
                            FileName = a.FileName,
                            ContentType = a.ContentType,
                            FileSize = a.FileSize
                        }).ToList(),
                        Reactions = m.Reactions.Where(r => !r.Deleted).GroupBy(r => r.Emoji).Select(g => new TicketReactionDto
                        {
                            Emoji = g.Key,
                            Count = g.Count(),
                            CurrentUserReacted = g.Any(r => r.UserId == currentUserId)
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            return ticket;
        }

        public async Task<TicketDto> CreateTicketAsync(CreateTicketDto dto, List<(string fileName, string contentType, byte[] data)> files, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<TicketType>(dto.Type, true, out var ticketType))
                throw new ArgumentException($"Invalid ticket type: {dto.Type}");

            var ticket = new Ticket(null, dto.Title, dto.Description, ticketType, _userAccessor.UserId);
            _context.Tickets.Add(ticket);

            if (files != null)
            {
                foreach (var (fileName, contentType, data) in files)
                {
                    var storagePath = await _fileAccessor.SaveAsync(ticket.Id, fileName, contentType, data);
                    var attachment = new TicketAttachment(null, fileName, contentType, storagePath, data.Length, ticketId: ticket.Id);
                    _context.TicketAttachments.Add(attachment);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return await GetTicketByIdAsync(ticket.Id, cancellationToken);
        }

        public async Task<TicketDto> UpdateTicketAsync(Guid id, UpdateTicketDto dto, CancellationToken cancellationToken = default)
        {
            var ticket = await _context.Tickets.FindAsync([id], cancellationToken)
                ?? throw new KeyNotFoundException($"Ticket {id} not found");

            var currentUserId = _userAccessor.UserId;
            var isStaff = _userAccessor.GetUserRoles().Contains("Staff") || _userAccessor.GetUserRoles().Contains("Admin");
            var isCreator = ticket.CreatorId == currentUserId;

            if (!isStaff && !isCreator)
                throw new UnauthorizedAccessException("Only the creator or staff can update this ticket");

            if (dto.Title != null || dto.Description != null)
            {
                ticket.Update(dto.Title ?? ticket.Title, dto.Description ?? ticket.Description);
            }

            if (isStaff)
            {
                TicketStatus? statusEnum = null;
                if (!string.IsNullOrEmpty(dto.Status) && Enum.TryParse<TicketStatus>(dto.Status, true, out var s))
                    statusEnum = s;

                TicketEffort? effortEnum = null;
                if (!string.IsNullOrEmpty(dto.Effort) && Enum.TryParse<TicketEffort>(dto.Effort, true, out var e))
                    effortEnum = e;

                ticket.UpdateAdminFields(statusEnum, dto.AdminPriority, effortEnum, dto.EstimatedMinutes, dto.SpentMinutes);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return await GetTicketByIdAsync(id, cancellationToken);
        }

        public async Task DeleteTicketAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var ticket = await _context.Tickets.FindAsync([id], cancellationToken)
                ?? throw new KeyNotFoundException($"Ticket {id} not found");

            var currentUserId = _userAccessor.UserId;
            var isStaff = _userAccessor.GetUserRoles().Contains("Staff") || _userAccessor.GetUserRoles().Contains("Admin");

            if (!isStaff && ticket.CreatorId != currentUserId)
                throw new UnauthorizedAccessException("Only the creator or staff can delete this ticket");

            _context.Remove(ticket);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TicketMessageDto> AddMessageAsync(Guid ticketId, string content, List<(string fileName, string contentType, byte[] data)> files, CancellationToken cancellationToken = default)
        {
            var message = new TicketMessage(null, ticketId, _userAccessor.UserId, content);
            _context.TicketMessages.Add(message);

            if (files != null)
            {
                foreach (var (fileName, contentType, data) in files)
                {
                    var storagePath = await _fileAccessor.SaveAsync(ticketId, fileName, contentType, data);
                    var attachment = new TicketAttachment(null, fileName, contentType, storagePath, data.Length, messageId: message.Id);
                    _context.TicketAttachments.Add(attachment);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            var currentUserId = _userAccessor.UserId;
            return await _context.TicketMessages
                .Where(m => m.Id == message.Id)
                .Select(m => new TicketMessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    UserId = m.UserId,
                    UserDisplayName = m.User.Person != null
                        ? (m.User.Person.GivenName + " " + m.User.Person.Surname).Trim()
                        : m.User.UserName,
                    SentAt = m.SentAt,
                    Attachments = m.Attachments.Where(a => !a.Deleted).Select(a => new TicketAttachmentDto
                    {
                        Id = a.Id,
                        FileName = a.FileName,
                        ContentType = a.ContentType,
                        FileSize = a.FileSize
                    }).ToList(),
                    Reactions = new List<TicketReactionDto>()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TicketListItemDto> VoteAsync(Guid ticketId, int value, CancellationToken cancellationToken = default)
        {
            if (value != 1 && value != -1 && value != 0)
                throw new ArgumentException("Vote value must be 1, -1, or 0");

            var currentUserId = _userAccessor.UserId;
            var existingVote = await _context.TicketVotes
                .FirstOrDefaultAsync(v => v.TicketId == ticketId && v.UserId == currentUserId && !v.Deleted, cancellationToken);

            if (value == 0 && existingVote != null)
            {
                _context.Remove(existingVote);
            }
            else if (existingVote != null)
            {
                existingVote.UpdateValue(value);
            }
            else if (value != 0)
            {
                var vote = new TicketVote(null, ticketId, currentUserId, value);
                _context.TicketVotes.Add(vote);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return await _context.Tickets
                .Where(t => t.Id == ticketId)
                .Select(t => new TicketListItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Type = t.Type.ToString(),
                    Status = t.Status.ToString(),
                    AdminPriority = t.AdminPriority,
                    Effort = t.Effort.HasValue ? t.Effort.Value.ToString() : null,
                    CreatorId = t.CreatorId,
                    CreatorDisplayName = t.Creator.Person != null
                        ? (t.Creator.Person.GivenName + " " + t.Creator.Person.Surname).Trim()
                        : t.Creator.UserName,
                    CreatedAt = t.CreatedAt,
                    VoteScore = t.Votes.Where(v => !v.Deleted).Sum(v => v.Value),
                    CurrentUserVote = t.Votes.Where(v => !v.Deleted && v.UserId == currentUserId).Select(v => (int?)v.Value).FirstOrDefault(),
                    MessageCount = t.Messages.Count(m => !m.Deleted),
                    HasUnread = false
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<TicketReactionDto>> ToggleReactionAsync(Guid messageId, string emoji, CancellationToken cancellationToken = default)
        {
            var currentUserId = _userAccessor.UserId;
            var existing = await _context.TicketReactions
                .FirstOrDefaultAsync(r => r.MessageId == messageId && r.UserId == currentUserId && r.Emoji == emoji && !r.Deleted, cancellationToken);

            if (existing != null)
            {
                _context.Remove(existing);
            }
            else
            {
                _context.TicketReactions.Add(new TicketReaction(null, messageId, currentUserId, emoji));
            }

            await _context.SaveChangesAsync(cancellationToken);

            return await _context.TicketReactions
                .Where(r => r.MessageId == messageId && !r.Deleted)
                .GroupBy(r => r.Emoji)
                .Select(g => new TicketReactionDto
                {
                    Emoji = g.Key,
                    Count = g.Count(),
                    CurrentUserReacted = g.Any(r => r.UserId == currentUserId)
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<TicketLinkDto> AddLinkAsync(Guid ticketId, CreateTicketLinkDto dto, CancellationToken cancellationToken = default)
        {
            var ticket = await _context.Tickets.FindAsync([ticketId], cancellationToken)
                ?? throw new KeyNotFoundException($"Ticket {ticketId} not found");

            var currentUserId = _userAccessor.UserId;
            var isStaff = _userAccessor.GetUserRoles().Contains("Staff") || _userAccessor.GetUserRoles().Contains("Admin");

            if (!isStaff && ticket.CreatorId != currentUserId)
                throw new UnauthorizedAccessException("Only the creator or staff can add links to this ticket");

            var link = new TicketLink(null, ticketId, dto.Label, dto.Url);
            _context.TicketLinks.Add(link);
            await _context.SaveChangesAsync(cancellationToken);

            return new TicketLinkDto { Id = link.Id, Label = link.Label, Url = link.Url };
        }

        public async Task RemoveLinkAsync(Guid linkId, CancellationToken cancellationToken = default)
        {
            var link = await _context.TicketLinks.FindAsync([linkId], cancellationToken)
                ?? throw new KeyNotFoundException($"Link {linkId} not found");

            var ticket = await _context.Tickets.FindAsync([link.TicketId], cancellationToken)
                ?? throw new KeyNotFoundException($"Ticket {link.TicketId} not found");

            var currentUserId = _userAccessor.UserId;
            var isStaff = _userAccessor.GetUserRoles().Contains("Staff") || _userAccessor.GetUserRoles().Contains("Admin");

            if (!isStaff && ticket.CreatorId != currentUserId)
                throw new UnauthorizedAccessException("Only the creator or staff can remove links from this ticket");

            _context.Remove(link);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task MarkAsReadAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            var currentUserId = _userAccessor.UserId;
            var existing = await _context.TicketReadStatuses
                .FirstOrDefaultAsync(rs => rs.TicketId == ticketId && rs.UserId == currentUserId && !rs.Deleted, cancellationToken);

            if (existing != null)
            {
                existing.MarkAsRead();
            }
            else
            {
                _context.TicketReadStatuses.Add(new TicketReadStatus(null, ticketId, currentUserId));
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TicketStatsDto> GetStatsAsync(CancellationToken cancellationToken = default)
        {
            var stats = await _context.Tickets
                .GroupBy(_ => 1)
                .Select(g => new TicketStatsDto
                {
                    Total = g.Count(),
                    Open = g.Count(t => t.Status == TicketStatus.Open),
                    InProgress = g.Count(t => t.Status == TicketStatus.InProgress),
                    Done = g.Count(t => t.Status == TicketStatus.Done),
                    Rejected = g.Count(t => t.Status == TicketStatus.Rejected)
                })
                .FirstOrDefaultAsync(cancellationToken);

            return stats ?? new TicketStatsDto();
        }

        public async Task<int> GetUnreadCountAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = _userAccessor.UserId;

            return await _context.Tickets
                .CountAsync(t =>
                    !t.ReadStatuses.Any(rs => !rs.Deleted && rs.UserId == currentUserId)
                    || t.Messages.Any(m => !m.Deleted && m.SentAt > t.ReadStatuses
                        .Where(rs => !rs.Deleted && rs.UserId == currentUserId)
                        .Select(rs => rs.LastReadAt)
                        .FirstOrDefault()),
                    cancellationToken);
        }

        public async Task<(byte[] content, string fileName, string contentType)?> GetAttachmentFileAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var attachment = await _context.TicketAttachments
                .FirstOrDefaultAsync(a => a.Id == attachmentId && !a.Deleted, cancellationToken);

            if (attachment == null)
                return null;

            var result = await _fileAccessor.GetAsync(attachment.StoragePath);
            if (result == null)
                return null;

            return (result.Content, attachment.FileName, attachment.ContentType);
        }
    }
}
