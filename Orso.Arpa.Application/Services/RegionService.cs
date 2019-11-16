using System;
using System.Collections.Generic;
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

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RegionDto>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RegionDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task ModifyAsync(RegionModifyDto modifyDto)
        {
            throw new NotImplementedException();
        }
    }
}
