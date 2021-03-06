using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentDto : BaseEntityDto
    {
        public Guid? CategoryId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string PublicDetails { get; set; }
        public string InternalDetails { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? EmolumentId { get; set; }
        public Guid? EmolumentPatternId { get; set; }
        public Guid? ExpectationId { get; set; }
        public Guid? VenueId { get; set; }
        public IList<RoomDto> Rooms { get; set; } = new List<RoomDto>();
        public IList<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
        public IList<SectionDto> Sections { get; set; } = new List<SectionDto>();
        public IList<AppointmentParticipationListItemDto> Participations { get; set; } = new List<AppointmentParticipationListItemDto>();
    }

    public class AppointmentDtoMappingProfile : Profile
    {
        public AppointmentDtoMappingProfile()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.Participations, opt => opt.MapFrom(src => src.AppointmentParticipations))
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.ProjectAppointments.Select(pa => pa.Project)))
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.SectionAppointments.Select(ra => ra.Section)))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.AppointmentRooms.Select(ra => ra.Room)))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Participations, opt => opt.Ignore())
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
