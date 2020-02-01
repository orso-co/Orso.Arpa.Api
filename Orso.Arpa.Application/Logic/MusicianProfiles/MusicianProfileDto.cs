using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.MusicianProfiles
{
    public class MusicianProfileDto
    {
        public bool IsProfessional { get; set; }
        public string SectionName { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MusicianProfile, MusicianProfileDto>()
                .ForMember(dest => dest.IsProfessional, opt => opt.MapFrom(src => src.IsProfessional))
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section.Name));
        }
    }
}
