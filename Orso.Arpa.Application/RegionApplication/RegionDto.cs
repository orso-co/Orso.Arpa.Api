using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.RegionApplication
{
    public class RegionDto : BaseEntityDto
    {
        [Translate(nameof(RegionDto))]
        public string Name { get; set; }
        public bool IsForRehearsal { get; set; }
        public bool IsForPerformance { get; set; }
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
