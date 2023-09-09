using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.UrlApplication.Interfaces;
using Orso.Arpa.Application.UrlApplication.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Application.UrlApplication.Services
{
    public class UrlService : BaseService<
        UrlDto,
        Url,
        UrlCreateDto,
        CreateUrl.Command,
        UrlModifyDto,
        UrlModifyBodyDto,
        ModifyUrl.Command
        >, IUrlService
    {
        public UrlService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<UrlDto> AddRoleAsync(UrlAddRoleDto addRoleDto)
        {
            AddRoleToUrl.Command command = _mapper.Map<AddRoleToUrl.Command>(addRoleDto);
            await _mediator.Send(command);
            return await GetByIdAsync(addRoleDto.Id);
        }

        public async Task RemoveRoleAsync(UrlRemoveRoleDto removeRoleDto)
        {
            RemoveRoleFromUrl.Command command = _mapper.Map<RemoveRoleFromUrl.Command>(removeRoleDto);
            await _mediator.Send(command);
        }
    }
}
