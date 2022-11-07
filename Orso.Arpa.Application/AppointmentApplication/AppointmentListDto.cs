using System;
using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentListDto
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Name { get; set; }

        public Guid? StatusId { get; set; }
    }

    public class AppointmentListDtoMappingProfile : Profile
    {
        public AppointmentListDtoMappingProfile()
        {
            CreateMap<Appointment, AppointmentListDto>();
        }
    }
}
