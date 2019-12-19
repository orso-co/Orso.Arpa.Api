using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Venues;

namespace Orso.Arpa.Application.Services
{
    public class VenueService : IVenueService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public VenueService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VenueDto>> GetAsync()
        {
            IImmutableList<Venue> venues = await _mediator.Send(new List.Query());
            return _mapper.Map<IEnumerable<VenueDto>>(venues);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsAsync(Guid id)
        {
            IImmutableList<Room> rooms = await _mediator.Send(new Rooms.Query(id));
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }
    }
}
