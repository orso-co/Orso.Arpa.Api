using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Sections
{
    public class SectionDto : BaseEntityDto
    {
        public string Name { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Section, SectionDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();

            CreateMap<SectionDto, Section>();
        }
    }
}
