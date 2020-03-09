using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Application.Services;

namespace Orso.Arpa.Application.AppointmentParticipationApplication
{
    public class AppointmentParticipationListItemDto
    {
        public PersonDto Person { get; set; }
        public AppointmentParticipationDto Participation { get; set; }
        public IList<MusicianProfileDto> MusicianProfiles { get; set; } = new List<MusicianProfileDto>();
    }

    public class AppointmentParticipationListItemDtoProfile : Profile
    {
        public AppointmentParticipationListItemDtoProfile()
        {
            CreateMap<PersonGrouping, AppointmentParticipationListItemDto>()
                .ForMember(dest => dest.Participation, opt => opt.MapFrom(src => src.Participation))
                .ForMember(dest => dest.MusicianProfiles, opt => opt.MapFrom(src => src.Profiles))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person));
        }
    }
}
