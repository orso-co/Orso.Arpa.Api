using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Application.VenueApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Venues;

namespace Orso.Arpa.Application.Services
{
    public class VenueService : BaseService<
        VenueDto,
        Venue,
        VenueCreateDto,
        Create.Command,
        VenueModifyDto,
        Modify.Command>, IVenueService
    {
        public VenueService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<IEnumerable<VenueDto>> GetAsync()
        {
            return await base.GetAsync(orderBy: v => v.OrderBy(v => v.Address.Zip));
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsAsync(Guid id)
        {
            IImmutableList<Room> rooms = await _mediator.Send(new Rooms.Query(id));
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }
    }
}
