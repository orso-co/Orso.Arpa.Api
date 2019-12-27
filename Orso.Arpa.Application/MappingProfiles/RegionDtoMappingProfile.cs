using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Dtos.Extensions;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class RegionDtoMappingProfile : Profile
    {
        public RegionDtoMappingProfile()
        {
            CreateMap<Region, RegionDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToIsoString()));
        }
    }
}
