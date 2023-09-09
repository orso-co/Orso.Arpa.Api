using AutoMapper;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Model;

namespace Orso.Arpa.Application.MusicianProfileApplication.Model
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
