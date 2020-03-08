using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Application.VenueApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyAppointmentDto : BaseEntityDto
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
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
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToIsoString()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToIsoString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.ProjectAppointments.Select(pa => pa.Project)))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.AppointmentRooms.Select(pa => pa.Room)))
                .ForMember(dest => dest.PublicDetails, opt => opt.MapFrom(src => src.PublicDetails))
                .ForMember(dest => dest.Expectation, opt => opt.MapFrom(src => src.Expectation.SelectValue.Name));

            CreateMap<AppointmentParticipation, MyAppointmentDto>()
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result != null ? src.Result.SelectValue.Name : null))
                .ForMember(dest => dest.PredictionId, opt => opt.MapFrom(src => src.PredictionId))
                .ForAllOtherMembers(dest => dest.Ignore());
        }
    }
}
