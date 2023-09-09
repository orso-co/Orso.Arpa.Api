using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Application.VenueApplication.Interfaces;
using Orso.Arpa.Application.VenueApplication.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Queries;

namespace Orso.Arpa.Application.VenueApplication.Services
{
    public class VenueService : BaseService<
        VenueDto,
        Venue,
        VenueCreateDto,
        CreateVenue.Command,
        VenueModifyDto,
        VenueModifyBodyDto,
        ModifyVenue.Command>, IVenueService
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
