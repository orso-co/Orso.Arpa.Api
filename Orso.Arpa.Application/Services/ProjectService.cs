using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Projects;

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

        public async Task<IEnumerable<ProjectDto>> GetAsync(bool includeCompleted)
        {
            IImmutableList<Project> projects = await _mediator.Send(new List.Query { IncludeCompleted = includeCompleted });
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }
    }
}
