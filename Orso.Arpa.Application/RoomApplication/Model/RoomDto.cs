using System;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.RoomApplication
{
    public class RoomDto : BaseEntityDto
    {
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Name { get; set; }
        public Guid VenueId { get; set; }
    }

    public class RoomDtoMappingProfile : Profile
    {
        public RoomDtoMappingProfile()
        {
            CreateMap<Room, RoomDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
