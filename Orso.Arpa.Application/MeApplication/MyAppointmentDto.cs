using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Application.VenueApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyAppointmentDto : BaseEntityDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public IList<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
        public VenueDto Venue { get; set; }
        public IList<RoomDto> Rooms { get; set; } = new List<RoomDto>();
        public string PublicDetails { get; set; }
        public string Expectation { get; set; }
        public string Result { get; set; }
        public Guid? PredictionId { get; set; }
    }

    public class MyAppointmentDtoMappingProfile : Profile
    {
        public MyAppointmentDtoMappingProfile()
        {
            CreateMap<Appointment, MyAppointmentDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.ProjectAppointments.Select(pa => pa.Project)))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.AppointmentRooms.Select(pa => pa.Room)))
                .ForMember(dest => dest.PublicDetails, opt => opt.MapFrom(src => src.PublicDetails))
                .ForMember(dest => dest.Expectation, opt => opt.MapFrom(src => src.Expectation.SelectValue.Name));
        }
    }
}
