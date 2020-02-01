using System;
using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Rooms
{
    public class RoomDto : BaseEntityDto
    {
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Name { get; set; }
        public Guid VenueId { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
