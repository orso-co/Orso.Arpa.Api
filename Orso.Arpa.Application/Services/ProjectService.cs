using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Logic.Projects;

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
            IImmutableList<Domain.Entities.Project> projects = await _mediator.Send(new List.Query { IncludeCompleted = includeCompleted });
            return _mapper.Map<IEnumerable<Logic.Projects.ProjectDto>>(projects);
        }
    }
}
