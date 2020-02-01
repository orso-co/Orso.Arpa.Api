using System;
using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Addresses
{
    public class AddressDto : BaseEntityDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string UrbanDistrict { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public Guid RegionId { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
