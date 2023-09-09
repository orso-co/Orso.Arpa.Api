using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Application.VenueApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Application.MeApplication.Model
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
        public AppointmentParticipationResult? Result { get; set; }
        public AppointmentParticipationPrediction? Prediction { get; set; }
        public SelectValueDto Category { get; set; }
        public AppointmentStatus? Status { get; set; }
        public string CommentByPerformerInner { get; set; }
    }

    public class MyAppointmentDtoMappingProfile : Profile
    {
        public MyAppointmentDtoMappingProfile()
        {
            _ = CreateMap<Appointment, MyAppointmentDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.ProjectAppointments.Select(pa => pa.Project)))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.AppointmentRooms.Select(pa => pa.Room)))
                .ForMember(dest => dest.PublicDetails, opt => opt.MapFrom(src => src.PublicDetails))
                .ForMember(dest => dest.Expectation, opt => opt.MapFrom(src => src.Expectation.SelectValue.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            // CommentByPerformerInner, Result and Prediction will be set manually in service
        }
    }
}
