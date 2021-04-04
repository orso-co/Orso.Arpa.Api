using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Services
{
    public class UrlService : BaseService<
        UrlDto,
        Url,
        UrlCreateDto,
        Domain.Logic.Urls.Create.Command,
        UrlModifyDto,
        Domain.Logic.Urls.Modify.Command
        >, IUrlService
    {
        public UrlService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        Task IUrlService.AddAsync(Guid id, UrlCreateDto urlCreateDto)
        {
            throw new NotImplementedException();
        }
        Task IUrlService.PutAsync(Guid id, Guid urlId, UrlModifyDto urlModifyDto)
        {
            throw new NotImplementedException();
        }

        Task IUrlService.DeleteAsync(Guid id, Guid urlId)
        {
            throw new NotImplementedException();
        }

        Task IUrlService.AddRoleAsync(Guid id, Guid urlId, UrlAddRoleDto urlAddRoleDt)
        {
            throw new NotImplementedException();
        }

        Task IUrlService.RemoveRoleAsync(Guid id, Guid urlId, Guid roledId)
        {
            throw new NotImplementedException();
        }
    }
}
