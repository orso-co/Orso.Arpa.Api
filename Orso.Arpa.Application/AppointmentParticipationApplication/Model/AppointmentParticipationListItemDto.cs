using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Application.PersonApplication.Model;
using static Orso.Arpa.Domain.MusicianProfileDomain.Queries.ListParticipationsForAppointment;

namespace Orso.Arpa.Application.AppointmentParticipationApplication.Model
{
    public class AppointmentParticipationListItemDto
    {
        public ReducedPersonDto Person { get; set; }
        public AppointmentParticipationDto Participation { get; set; }
        public IList<ReducedMusicianProfileDto> MusicianProfiles { get; set; } = [];
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
