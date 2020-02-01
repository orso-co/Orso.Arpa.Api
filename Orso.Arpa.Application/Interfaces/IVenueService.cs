using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.Logic.Rooms;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IVenueService
    {
        Task<IEnumerable<Logic.Venues.VenueDto>> GetAsync();

        Task<IEnumerable<RoomDto>> GetRoomsAsync(Guid id);
    }
}
