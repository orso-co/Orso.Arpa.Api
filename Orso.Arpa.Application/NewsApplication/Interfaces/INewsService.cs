using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Application.NewsApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Application.NewsApplication.Interfaces;

public interface INewsService
{
    Task<NewsDto> CreateAsync(NewsCreateDto createDto);

    Task<IEnumerable<NewsDto>> GetAsync(
        int? limit,
        int? offset,
        bool includeHidden);

    Task<NewsDto> GetByIdAsync(Guid id);
    Task ModifyAsync(NewsModifyDto modifyDto);
    Task DeleteAsync(Guid id);
    Task MarkAsReadAsync(Guid newsId, CancellationToken cancellationToken = default);
    Task MarkAsUnreadAsync(Guid newsId, CancellationToken cancellationToken = default);
    Task<IFileResult> SetImageAsync(Guid newsId, IFormFile file);
    Task<IFileResult> GetImageAsync(Guid newsId);
    Task DeleteImageAsync(Guid newsId);
}
