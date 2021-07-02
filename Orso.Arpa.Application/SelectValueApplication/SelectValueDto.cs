using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.SelectValueApplication
{
    public class SelectValueDto : BaseEntityDto
    {
        [Translate(nameof(SelectValueDto))]
        public string Name { get; set; }
        [Translate(nameof(SelectValueDto))]
        public string Description { get; set; }
    }

    public class SelectValueDtoMappingProfile : Profile
    {
        public SelectValueDtoMappingProfile()
        {
            CreateMap<SelectValueMapping, SelectValueDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.SelectValue.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SelectValue.Name))
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
