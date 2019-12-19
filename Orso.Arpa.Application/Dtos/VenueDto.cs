using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.Dtos
{
    public class VenueDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AddressId { get; set; }
        public AddressDto Address { get; set; }
        public IEnumerable<RoomDto> Rooms { get; set; } = new List<RoomDto>();
    }
}
