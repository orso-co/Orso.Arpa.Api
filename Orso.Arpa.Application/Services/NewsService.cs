using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.NewsApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.News;

namespace Orso.Arpa.Application.Services;

public class NewsService :
    BaseService<NewsDto, News, NewsCreateDto, Create.Command,
        NewsModifyDto, NewsModifyBodyDto, Modify.Command>, INewsService
{
    public NewsService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    public async Task<IEnumerable<NewsDto>> GetAsync(
         int? limit,
         int? offset,
         bool includeHidden)

    {
        return await base.GetAsync(predicate: includeHidden ? null : n => n.Show,
            orderBy: v => v.OrderByDescending(n => n.CreatedAt),
            skip: offset ?? 0,
            take: limit ?? 25);
    }
}
