using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Logic.Regions;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class RegionCreateDtoMappingProfile : Profile
    {
        public RegionCreateDtoMappingProfile()
        {
            CreateMap<RegionCreateDto, Create.Command>();
        }
    }
}
