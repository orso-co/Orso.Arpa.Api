using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Urls;

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

        async Task IUrlService.PutAsync(UrlModifyDto urlModifyDto)
        {
            await ModifyAsync(urlModifyDto);
        }

        public async Task AddRoleAsync(UrlAddRoleDto addRoleDto)
        {
            AddRole.Command command = _mapper.Map<AddRole.Command>(addRoleDto);
            await _mediator.Send(command);
        }

        public async Task RemoveRoleAsync(UrlRemoveRoleDto removeRoleDto)
        {
            RemoveRole.Command command = _mapper.Map<RemoveRole.Command>(removeRoleDto);
            await _mediator.Send(command);
        }
    }
}
