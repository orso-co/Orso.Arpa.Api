using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Application.VenueApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IVenueService
    {
        Task<IEnumerable<VenueDto>> GetAsync();

        Task<IEnumerable<RoomDto>> GetRoomsAsync(Guid id);
    }
}
