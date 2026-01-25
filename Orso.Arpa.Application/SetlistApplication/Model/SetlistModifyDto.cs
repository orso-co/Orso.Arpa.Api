using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;

namespace Orso.Arpa.Application.SetlistApplication.Model
{
    public class SetlistModifyDto : IdFromRouteDto<SetlistModifyBodyDto>
    {
    }

    public class SetlistModifyBodyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsTemplate { get; set; }
    }

    public class SetlistModifyDtoMappingProfile : Profile
    {
        public SetlistModifyDtoMappingProfile()
        {
            CreateMap<SetlistModifyDto, ModifySetlist.Command>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.IsTemplate, opt => opt.MapFrom(src => src.Body.IsTemplate));
        }
    }
}
