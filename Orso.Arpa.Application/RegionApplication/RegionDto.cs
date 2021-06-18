using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Translation;

namespace Orso.Arpa.Application.RegionApplication
{
    public class RegionDto : BaseEntityDto
    {
        [Translate(nameof(RegionDto))]
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
