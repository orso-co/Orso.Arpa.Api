using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.Tranlation;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.RegionApplication
{
    public class RegionDto : BaseEntityDto
    {
        [Translate]
        public string Name { get; set; }
    }

    public class RegionDtoMappingProfile : Profile
    {
        public RegionDtoMappingProfile()
        {
            CreateMap<Region, RegionDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
