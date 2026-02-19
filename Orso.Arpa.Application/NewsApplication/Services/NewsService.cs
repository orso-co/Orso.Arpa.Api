using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.NewsApplication.Interfaces;
using Orso.Arpa.Application.NewsApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.NewsDomain.Commands;
using Orso.Arpa.Domain.NewsDomain.Interfaces;
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
    private readonly INewsImageAccessor _newsImageAccessor;
    private readonly IFileNameGenerator _fileNameGenerator;

    public NewsService(IMediator mediator, IMapper mapper, IArpaContext arpaContext, ITokenAccessor tokenAccessor,
        INewsImageAccessor newsImageAccessor, IFileNameGenerator fileNameGenerator)
        : base(mediator, mapper)
    {
        _arpaContext = arpaContext;
        _tokenAccessor = tokenAccessor;
        _newsImageAccessor = newsImageAccessor;
        _fileNameGenerator = fileNameGenerator;
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

        if (alreadyRead)
        {
            return;
        }

        // Hard delete any soft-deleted entries to avoid unique constraint violation
        await _arpaContext.NewsReadStatuses
            .IgnoreQueryFilters()
            .Where(r => r.NewsId == newsId && r.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);

        _arpaContext.Add(new NewsReadStatus(null, newsId, userId));
        await _arpaContext.SaveChangesAsync(cancellationToken);
    }

    public async Task MarkAsUnreadAsync(Guid newsId, CancellationToken cancellationToken = default)
    {
        Guid userId = _tokenAccessor.UserId;

        // Hard delete to free up the unique constraint for future re-reads
        await _arpaContext.NewsReadStatuses
            .IgnoreQueryFilters()
            .Where(r => r.NewsId == newsId && r.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);
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

    public async Task<IFileResult> SetImageAsync(Guid newsId, IFormFile file)
    {
        News news = await _arpaContext.Set<News>().FindAsync(newsId)
            ?? throw new KeyNotFoundException($"News with id '{newsId}' not found");

        if (!string.IsNullOrEmpty(news.ImageFileName))
        {
            await _newsImageAccessor.DeleteAsync(news.ImageFileName);
        }

        string fileName = _fileNameGenerator.GenerateRandomFileName(file);
        IFileResult result = await _newsImageAccessor.SaveAsync(file, fileName);
        news.SetImageFileName(fileName);
        await _arpaContext.SaveChangesAsync(default);
        return result;
    }

    public async Task<IFileResult> GetImageAsync(Guid newsId)
    {
        News news = await _arpaContext.Set<News>().FindAsync(newsId)
            ?? throw new KeyNotFoundException($"News with id '{newsId}' not found");

        if (string.IsNullOrEmpty(news.ImageFileName))
        {
            return null;
        }

        return await _newsImageAccessor.GetAsync(news.ImageFileName);
    }

    public async Task DeleteImageAsync(Guid newsId)
    {
        News news = await _arpaContext.Set<News>().FindAsync(newsId)
            ?? throw new KeyNotFoundException($"News with id '{newsId}' not found");

        if (!string.IsNullOrEmpty(news.ImageFileName))
        {
            await _newsImageAccessor.DeleteAsync(news.ImageFileName);
            news.ClearImageFileName();
            await _arpaContext.SaveChangesAsync(default);
        }
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
