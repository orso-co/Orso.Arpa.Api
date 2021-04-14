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
        Create.Command,
        UrlModifyDto,
        Modify.Command
        >, IUrlService
    {
        public UrlService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<UrlDto> AddUrlAsync(UrlCreateDto urlCreateDto)
        {
            Create.Command command = _mapper.Map<Create.Command>(urlCreateDto);
            Url createdUrl = await _mediator.Send(command);
            return _mapper.Map<UrlDto>(createdUrl);
        }

        public async Task<UrlDto> AddRoleAsync(UrlAddRoleDto addRoleDto)
        {
            AddRole.Command command = _mapper.Map<AddRole.Command>(addRoleDto);
            await _mediator.Send(command);
            return await GetByIdAsync(addRoleDto.Id);
        }

        public async Task RemoveRoleAsync(UrlRemoveRoleDto removeRoleDto)
        {
            RemoveRole.Command command = _mapper.Map<RemoveRole.Command>(removeRoleDto);
            await _mediator.Send(command);
        }
    }
}
