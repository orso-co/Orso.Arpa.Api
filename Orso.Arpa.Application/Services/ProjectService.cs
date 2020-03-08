using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Logic.Projects.ProjectDto>> GetAsync(bool includeCompleted)
        {
            Expression<Func<Project, bool>> predicate = includeCompleted ? default(Expression<Func<Project, bool>>) : p => !p.IsCompleted;
            IQueryable<Project> projects = await _mediator.Send(
                new Domain.GenericHandlers.List.Query<Project>(predicate));

            return projects.ProjectTo<Logic.Projects.ProjectDto>(_mapper.ConfigurationProvider);
        }
    }
}
