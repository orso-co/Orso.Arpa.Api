using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.InstrumentationDomain.Commands;

namespace Orso.Arpa.Application.InstrumentationApplication.Model
{
    public class InstrumentationModifyDto : IdFromRouteDto<InstrumentationModifyBodyDto>
    {
    }

    public class InstrumentationModifyBodyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsTemplate { get; set; }
    }

    public class InstrumentationModifyDtoMappingProfile : Profile
    {
        public InstrumentationModifyDtoMappingProfile()
        {
            CreateMap<InstrumentationModifyDto, ModifyInstrumentation.Command>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.IsTemplate, opt => opt.MapFrom(src => src.Body.IsTemplate));
        }
    }
}
