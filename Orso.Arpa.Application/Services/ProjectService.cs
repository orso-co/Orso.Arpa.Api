using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Generic = Orso.Arpa.Domain.GenericHandlers;

namespace Orso.Arpa.Application.Services
{
    public class ProjectService : BaseService<
        ProjectDto,
        Project,
        // TODO UrlDto,
        // TODO Domain.Logic.Urls.Create.Command,
        // TODO Domain.Logic.Urls.Modify.Command
        ProjectCreateDto,
        Domain.Logic.Projects.Create.Command,
        ProjectModifyDto,
        Domain.Logic.Projects.Modify.Command
        >, IProjectService
    {
        public ProjectService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<IEnumerable<ProjectDto>> GetAsync(bool includeCompleted)
        {
            Expression<Func<Project, bool>> predicate = includeCompleted ? default : p => !p.IsCompleted;

            return await base.GetAsync(predicate: predicate);
        }
        public override async Task<ProjectDto> GetByIdAsync(Guid id)
        {
            Project Project = await _mediator.Send(new Generic.Details.Query<Project>(id));
            ProjectDto dto = _mapper.Map<ProjectDto>(Project);
            return dto;
        }
        public Task PostUrlAsync(Guid id, UrlDto urlDto)
        {
            //TODO
            //Url.Command command = _mapper.Map<Url.Command>(UrlDto);
            //await _mediator.Send(command);
            throw new NotImplementedException();
        }

        public Task PutUrlAsync(Guid id, Guid urlId, UrlDto urlDto)
        {
            //TODO
            throw new NotImplementedException();
        }

        public Task DeleteUrlAsync(Guid id, Guid urlId)
        {
            //TODO
            throw new NotImplementedException();
        }

    }
}
