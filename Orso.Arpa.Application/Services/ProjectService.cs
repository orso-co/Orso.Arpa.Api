using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.ProjectParticipations;

namespace Orso.Arpa.Application.Services
{
    public class ProjectService : BaseService<
        ProjectDto,
        Project,
        ProjectCreateDto,
        Domain.Logic.Projects.Create.Command,
        ProjectModifyDto,
        ProjectModifyBodyDto,
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

        public async Task<IEnumerable<ProjectParticipationDto>> GetParticipationsByIdAsync(Guid id)
        {
            var query = new GetForProject.Query { ProjectId = id };
            IOrderedQueryable<ProjectParticipation> projectParticipations = await _mediator.Send(query);

            return await projectParticipations
                .ProjectTo<ProjectParticipationDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
