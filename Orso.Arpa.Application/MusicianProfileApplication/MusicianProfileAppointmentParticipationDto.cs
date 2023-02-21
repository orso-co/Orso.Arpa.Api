using AutoMapper;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MusicianProfileAppointmentParticipationDto
    {
        public AppointmentParticipationDto AppointmentParticipation { get; set; }
        public AppointmentListDto Appointment { get; set; }
    }

    public class MusicianProfileAppointmentParticipationDtoMappingProfile : Profile
    {
        public MusicianProfileAppointmentParticipationDtoMappingProfile()
        {
            _ = CreateMap<AppointmentParticipation, MusicianProfileAppointmentParticipationDto>()
                .ForMember(dest => dest.AppointmentParticipation, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Appointment, opt => opt.MapFrom(src => src.Appointment));
        }
    }
}
