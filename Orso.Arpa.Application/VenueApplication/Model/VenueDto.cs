using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.AddressApplication.Model;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Application.VenueApplication.Model
{
    public class VenueDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AddressId { get; set; }
        public AddressDto Address { get; set; }
        public IList<RoomDto> Rooms { get; set; } = [];
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
