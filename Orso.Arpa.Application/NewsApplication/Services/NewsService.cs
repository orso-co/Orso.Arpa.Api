using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.NewsApplication.Interfaces;
using Orso.Arpa.Application.NewsApplication.Model;
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
