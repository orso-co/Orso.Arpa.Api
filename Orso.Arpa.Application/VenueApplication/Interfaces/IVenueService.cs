using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Application.VenueApplication.Model;

namespace Orso.Arpa.Application.VenueApplication.Interfaces
{
    public interface IVenueService
    {
        Task<IEnumerable<VenueDto>> GetAsync();

        Task<IEnumerable<RoomDto>> GetRoomsAsync(Guid id);

        Task ModifyAsync(VenueModifyDto modifyDto);

        Task<VenueDto> CreateAsync(VenueCreateDto createDto);

        Task DeleteAsync(Guid id);
    }
}
