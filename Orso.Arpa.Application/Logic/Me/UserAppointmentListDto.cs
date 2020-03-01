using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Me
{
    public class UserAppointmentListDto
    {
        public IList<UserAppointmentDto> UserAppointments { get; set; } = new List<UserAppointmentDto>();
        public int TotalRecordsCount { get; set; }
    }

    public class UserAppointmentListDtoMappingProfile : MappingProfile
    {
        public UserAppointmentListDtoMappingProfile()
        {
            CreateMap<Tuple<IEnumerable<Appointment>, int>, UserAppointmentListDto>()
                .ForMember(dest => dest.UserAppointments, opt => opt.MapFrom(src => src.Item1))
                .ForMember(dest => dest.TotalRecordsCount, opt => opt.MapFrom(src => src.Item2));
        }
    }
}
