using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using AutoMapper;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class AppointmentDto : BaseEntityDto
    {
        public Guid? CategoryId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Name { get; set; }

        public string PublicDetails { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public string InternalDetails { get; set; }

        public AppointmentStatus? Status { get; set; }

        public Guid? SalaryId { get; set; }

        public Guid? SalaryPatternId { get; set; }

        public Guid? ExpectationId { get; set; }

        public Guid? VenueId { get; set; }

        public IList<RoomDto> Rooms { get; set; } = [];

        public IList<ProjectDto> Projects { get; set; } = [];

        public IList<SectionDto> Sections { get; set; } = [];

        public IList<AppointmentParticipationListItemDto> Participations { get; set; } = [];
    }

    public class AppointmentDtoMappingProfile : Profile
    {
        public AppointmentDtoMappingProfile()
        {
            _ = CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.Participations, opt => opt.Ignore())
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.ProjectAppointments.Select(pa => pa.Project)))
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.SectionAppointments.Select(ra => ra.Section)))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.AppointmentRooms.Select(ra => ra.Room)))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .AfterMap<RoleBasedSetNullAction<Appointment, AppointmentDto>>();
        }
    }
}
