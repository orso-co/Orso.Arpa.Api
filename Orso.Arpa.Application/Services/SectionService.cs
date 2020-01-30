using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Sections;

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
            IImmutableList<Section> sections = await _mediator.Send(new List.Query());
            return _mapper.Map<IEnumerable<SectionDto>>(sections);
        }
    }
}
