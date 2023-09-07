using System;
using AutoMapper;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Application.AppointmentApplication
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
