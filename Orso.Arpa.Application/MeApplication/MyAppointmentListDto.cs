using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyAppointmentListDto
    {
        public IList<MyAppointmentDto> UserAppointments { get; set; } = new List<MyAppointmentDto>();
        public int TotalRecordsCount { get; set; }
    }

    public class MyAppointmentListDtoMappingProfile : MyUserProfileDtoMappingProfile
    {
        public MyAppointmentListDtoMappingProfile()
        {
            CreateMap<Tuple<IEnumerable<Appointment>, int>, MyAppointmentListDto>()
                .ForMember(dest => dest.UserAppointments, opt => opt.MapFrom(src => src.Item1))
                .ForMember(dest => dest.TotalRecordsCount, opt => opt.MapFrom(src => src.Item2));
        }
    }
}
