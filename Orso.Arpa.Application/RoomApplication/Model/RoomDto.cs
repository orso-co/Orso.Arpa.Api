using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Application.RoomApplication.Model
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
