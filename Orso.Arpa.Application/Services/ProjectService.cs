using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Services
{
    public class ProjectService : BaseService<
        ProjectDto,
        Project,
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
    }
}
