using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionDto : BaseEntityDto
    {
        public string Name { get; set; }
    }

    public class SectionDtoMappingProfile : Profile
    {
        public SectionDtoMappingProfile()
        {
            CreateMap<Section, SectionDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();

            CreateMap<SectionDto, Section>();
        }
    }
}
