using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.NewsApplication.Model;

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
}
