using AutoMapper;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.SelectValueApplication.Model
{
    public class SelectValueDto : BaseEntityDto
    {
        [Translate(LocalizationKeys.SELECT_VALUE)]
        public string Name { get; set; }
        [Translate(LocalizationKeys.SELECT_VALUE)]
        public string Description { get; set; }
    }

    public class SelectValueDtoMappingProfile : Profile
    {
        public SelectValueDtoMappingProfile()
        {
            _ = CreateMap<SelectValueMapping, SelectValueDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.SelectValue.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SelectValue.Name))
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .AfterMap<LocalizeAction<SelectValueMapping, SelectValueDto>>();

            _ = CreateMap<SelectValueSection, SelectValueDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.SelectValue.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SelectValue.Name))
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .AfterMap<LocalizeAction<SelectValueSection, SelectValueDto>>();
        }
    }
}
