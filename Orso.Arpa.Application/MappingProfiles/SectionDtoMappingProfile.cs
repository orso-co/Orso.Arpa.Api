using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
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
