using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class AppointmentListDto
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public AppointmentStatus? Status { get; set; }

        [Translate(LocalizationKeys.SELECT_VALUE)]
        public string Category { get; set; }

        public IEnumerable<ReducedProjectDto> Projects { get; set; } = [];
    }

    public class AppointmentListDtoMappingProfile : Profile
    {
        public AppointmentListDtoMappingProfile()
        {
            _ = CreateMap<Appointment, AppointmentListDto>()
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.Venue != null && src.Venue.Address != null ? src.Venue.Address.City : null))
                .ForMember(dto => dto.Category, opt => opt.MapFrom(src => src.Category != null && src.Category.SelectValue != null ? src.Category.SelectValue.Name : null))
                .ForMember(dto => dto.Projects, opt => opt.MapFrom(src => src.ProjectAppointments.Select(pa => pa.Project)))
                .AfterMap<LocalizeAction<Appointment, AppointmentListDto>>();
        }
    }
}
