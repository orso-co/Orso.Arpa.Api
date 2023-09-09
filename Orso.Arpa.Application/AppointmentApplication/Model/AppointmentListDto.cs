using System;
using AutoMapper;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class AppointmentListDto
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Name { get; set; }

        public AppointmentStatus? Status { get; set; }
    }

    public class AppointmentListDtoMappingProfile : Profile
    {
        public AppointmentListDtoMappingProfile()
        {
            _ = CreateMap<Appointment, AppointmentListDto>();
        }
    }
}
