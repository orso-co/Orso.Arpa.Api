using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Logic.Rooms;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Venues
{
    public class VenueDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AddressId { get; set; }
        public Addresses.AddressDto Address { get; set; }
        public IList<RoomDto> Rooms { get; set; } = new List<RoomDto>();
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Venue, VenueDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
