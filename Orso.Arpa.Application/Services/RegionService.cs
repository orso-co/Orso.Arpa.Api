using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.Regions;
using Orso.Arpa.Domain.Entities;

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

        public async Task<RegionDto> CreateAsync(Logic.Regions.Create.Dto createDto)
        {
            Domain.Logic.Regions.Create.Command command = _mapper.Map<Domain.Logic.Regions.Create.Command>(createDto);
            Region createdRegion = await _mediator.Send(command);
            return _mapper.Map<RegionDto>(createdRegion);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new Domain.GenericHandlers.Delete.Command<Region>() { Id = id });
        }

        public async Task<IEnumerable<RegionDto>> GetAsync()
        {
            IQueryable<Region> regions = await _mediator.Send(new Domain.GenericHandlers.List.Query<Region>());
            return regions.ProjectTo<RegionDto>(_mapper.ConfigurationProvider);
        }

        public async Task<RegionDto> GetByIdAsync(Guid id)
        {
            Region region = await _mediator.Send(new Domain.GenericHandlers.Details.Query<Region>(id));
            return _mapper.Map<RegionDto>(region);
        }

        public async Task ModifyAsync(Logic.Regions.Modify.Dto modifyDto)
        {
            Domain.Logic.Regions.Modify.Command command = _mapper.Map<Domain.Logic.Regions.Modify.Command>(modifyDto);
            await _mediator.Send(command);
        }
    }
}
