using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class AddressDtoMappingProfile : Profile
    {
        public AddressDtoMappingProfile()
        {
            CreateMap<Address, AddressDto>();
        }
    }
}
