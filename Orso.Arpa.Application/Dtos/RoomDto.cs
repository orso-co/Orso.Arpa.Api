using System;

namespace Orso.Arpa.Application.Dtos
{
    public class RoomDto : BaseEntityDto
    {
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Name { get; set; }
        public Guid VenueId { get; set; }
    }
}
