using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Logic.Regions;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class RegionModifyDtoMappingProfile : Profile
    {
        public RegionModifyDtoMappingProfile()
        {
            CreateMap<RegionModifyDto, Modify.Command>();
        }
    }
}
