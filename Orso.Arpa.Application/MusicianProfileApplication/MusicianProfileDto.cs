using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MusicianProfileDto
    {
        public string SectionName { get; set; }
        public string Qualification { get; set; }
    }

    public class MusicianProfileDtoMappingProfile : Profile
    {
        public MusicianProfileDtoMappingProfile()
        {
            CreateMap<MusicianProfile, MusicianProfileDto>()
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification.SelectValue.Name))
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Instrument.Name));
        }
    }
}
