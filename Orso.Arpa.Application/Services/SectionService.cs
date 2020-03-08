using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.Sections;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Services
{
    public class SectionService : ISectionService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SectionService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SectionDto>> GetAsync()
        {
            IQueryable<Section> sections = await _mediator
                .Send(new Domain.GenericHandlers.List.Query<Section>(orderBy: s => s.OrderBy(s => s.Name)));
            return sections.ProjectTo<SectionDto>(_mapper.ConfigurationProvider);
        }
    }
}
