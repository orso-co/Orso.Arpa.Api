using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.Rooms;
using Orso.Arpa.Application.Logic.Venues;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Venues;

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
            IQueryable<Venue> venues = await _mediator
                .Send(new Domain.GenericHandlers.List.Query<Venue>(orderBy: v => v.OrderBy(v => v.Address.City)));
            return venues.ProjectTo<VenueDto>(_mapper.ConfigurationProvider);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsAsync(Guid id)
        {
            IImmutableList<Room> rooms = await _mediator.Send(new Rooms.Query(id));
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }
    }
}
