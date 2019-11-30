using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Regions;

namespace Orso.Arpa.Application.Services
{
    public class RegionService : IRegionService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RegionService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<RegionDto> CreateAsync(RegionCreateDto createDto)
        {
            Create.Command command = _mapper.Map<Create.Command>(createDto);
            Region createdRegion = await _mediator.Send(command);
            return _mapper.Map<RegionDto>(createdRegion);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new Delete.Command() { Id = id });
        }

        public async Task<IEnumerable<RegionDto>> GetAsync()
        {
            IImmutableList<Region> regions = await _mediator.Send(new List.Query());
            return _mapper.Map<IEnumerable<RegionDto>>(regions);
        }

        public async Task<RegionDto> GetByIdAsync(Guid id)
        {
            Region region = await _mediator.Send(new Details.Query { Id = id });
            return _mapper.Map<RegionDto>(region);
        }

        public async Task ModifyAsync(RegionModifyDto modifyDto)
        {
            Modify.Command command = _mapper.Map<Modify.Command>(modifyDto);
            await _mediator.Send(command);
        }
    }
}
