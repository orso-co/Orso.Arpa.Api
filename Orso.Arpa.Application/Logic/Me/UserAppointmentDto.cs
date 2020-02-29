using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.Logic.Projects;
using Orso.Arpa.Application.Logic.Rooms;
using Orso.Arpa.Application.Logic.Venues;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Me
{
    public class UserAppointmentDto : BaseEntityDto
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Name { get; set; }
        public IList<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
        public VenueDto Venue { get; set; }
        public IList<RoomDto> Rooms { get; set; } = new List<RoomDto>();
        public string PublicDetails { get; set; }
        public string Expectation { get; set; }
    }

    public class UserAppointmentDtoMappingProfile : Profile
    {
        public UserAppointmentDtoMappingProfile()
        {
            CreateMap<Appointment, UserAppointmentDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToIsoString()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToIsoString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.ProjectAppointments.Select(pa => pa.Project)))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.AppointmentRooms.Select(pa => pa.Room)))
                .ForMember(dest => dest.PublicDetails, opt => opt.MapFrom(src => src.PublicDetails))
                .ForMember(dest => dest.Expectation, opt => opt.MapFrom(src => src.Expectation.SelectValue.Name));
        }
    }
}
