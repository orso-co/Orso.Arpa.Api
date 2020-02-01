using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.SelectValues
{
    public class SelectValueDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SelectValueMapping, SelectValueDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.SelectValue.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SelectValue.Name))
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
