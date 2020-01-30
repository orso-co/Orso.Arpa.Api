using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class AddSectionDtoMappingProfile : Profile
    {
        public AddSectionDtoMappingProfile()
        {
            CreateMap<AddSectionDto, AddSection.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SectionId, opt => opt.MapFrom(src => src.SectionId));
        }
    }
}
