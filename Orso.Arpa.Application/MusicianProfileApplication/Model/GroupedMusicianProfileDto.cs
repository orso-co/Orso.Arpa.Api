using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.MusicianProfileApplication.Model
{
    public class GroupedMusicianProfileDto
    {
        public ReducedPersonDto Person { get; set; }
        public IList<ReducedMusicianProfileDto> MusicianProfiles { get; set; }
    }

    public class GroupedMusicianProfileDtoMappingProfile : Profile
    {
        public GroupedMusicianProfileDtoMappingProfile()
        {
            CreateMap<Person, GroupedMusicianProfileDto>()
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.MusicianProfiles, opt => opt.MapFrom(src => src.MusicianProfiles));
        }
    }
}
