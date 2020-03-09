using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.AddressApplication;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.VenueApplication
{
    public class VenueDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AddressId { get; set; }
        public AddressDto Address { get; set; }
        public IList<RoomDto> Rooms { get; set; } = new List<RoomDto>();
    }

    public class VenueDtoMappingProfile : Profile
    {
        public VenueDtoMappingProfile()
        {
            CreateMap<Venue, VenueDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
