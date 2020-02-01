using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Regions
{
    public class RegionDto : BaseEntityDto
    {
        public string Name { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Region, RegionDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
