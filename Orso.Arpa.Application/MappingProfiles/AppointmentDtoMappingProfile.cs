using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Dtos.Extensions;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class AppointmentDtoMappingProfile : Profile
    {
        public AppointmentDtoMappingProfile()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.Participations, opt => opt.MapFrom(src => src.AppointmentParticipations))
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.ProjectAppointments.Select(pa => pa.Project)))
                .ForMember(dest => dest.Registers, opt => opt.MapFrom(src => src.RegisterAppointments.Select(ra => ra.Register)))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.AppointmentRooms.Select(ra => ra.Room)))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToIsoString()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToIsoString()));
        }
    }
}
