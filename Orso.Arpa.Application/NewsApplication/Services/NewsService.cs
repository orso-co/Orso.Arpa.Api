using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.NewsApplication.Interfaces;
using Orso.Arpa.Application.NewsApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.NewsDomain.Commands;
using Orso.Arpa.Domain.NewsDomain.Model;

namespace Orso.Arpa.Application.NewsApplication.Services;

public class NewsService :
    BaseService<
        NewsDto,
        News,
        NewsCreateDto,
        CreateNews.Command,
        NewsModifyDto,
        NewsModifyBodyDto,
        ModifyNews.Command>, INewsService
{
    private readonly IArpaContext _arpaContext;
    private readonly ITokenAccessor _tokenAccessor;

    public NewsService(IMediator mediator, IMapper mapper, IArpaContext arpaContext, ITokenAccessor tokenAccessor)
        : base(mediator, mapper)
    {
        _arpaContext = arpaContext;
        _tokenAccessor = tokenAccessor;
    }

    public async Task<IEnumerable<NewsDto>> GetAsync(
         int? limit,
         int? offset,
         bool includeHidden)
    {
        IEnumerable<NewsDto> newsDtos = await base.GetAsync(
            predicate: includeHidden ? null : n => n.Show,
            orderBy: v => v.OrderByDescending(n => n.CreatedAt),
            skip: offset ?? 0,
            take: limit);

        return await EnrichWithReadStatus(newsDtos);
    }

    public override async Task<NewsDto> GetByIdAsync(Guid id)
    {
        NewsDto dto = await base.GetByIdAsync(id);
        return await EnrichWithReadStatus(dto);
    }

    public async Task MarkAsReadAsync(Guid newsId, CancellationToken cancellationToken = default)
    {
        Guid userId = _tokenAccessor.UserId;

        bool alreadyRead = await _arpaContext.NewsReadStatuses
            .AnyAsync(r => r.NewsId == newsId && r.UserId == userId, cancellationToken);

        if (!alreadyRead)
        {
            _arpaContext.Add(new NewsReadStatus(null, newsId, userId));
            await _arpaContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task MarkAsUnreadAsync(Guid newsId, CancellationToken cancellationToken = default)
    {
        Guid userId = _tokenAccessor.UserId;

        NewsReadStatus readStatus = await _arpaContext.NewsReadStatuses
            .FirstOrDefaultAsync(r => r.NewsId == newsId && r.UserId == userId, cancellationToken);

        if (readStatus != null)
        {
            _arpaContext.Remove(readStatus);
            await _arpaContext.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task<IEnumerable<NewsDto>> EnrichWithReadStatus(IEnumerable<NewsDto> newsDtos)
    {
        Guid userId = _tokenAccessor.UserId;
        List<NewsDto> dtoList = newsDtos.ToList();
        List<Guid> newsIds = dtoList.Select(n => n.Id).ToList();

        Dictionary<Guid, DateTime> readStatuses = await _arpaContext.NewsReadStatuses
            .Where(r => r.UserId == userId && newsIds.Contains(r.NewsId))
            .ToDictionaryAsync(r => r.NewsId, r => r.ReadAt);

        foreach (NewsDto dto in dtoList)
        {
            if (readStatuses.TryGetValue(dto.Id, out DateTime readAt))
            {
                dto.IsRead = true;
                dto.ReadAt = readAt;
            }
        }

        return dtoList;
    }

    private async Task<NewsDto> EnrichWithReadStatus(NewsDto dto)
    {
        Guid userId = _tokenAccessor.UserId;

        NewsReadStatus readStatus = await _arpaContext.NewsReadStatuses
            .FirstOrDefaultAsync(r => r.UserId == userId && r.NewsId == dto.Id);

        if (readStatus != null)
        {
            dto.IsRead = true;
            dto.ReadAt = readStatus.ReadAt;
        }

        return dto;
    }
}
