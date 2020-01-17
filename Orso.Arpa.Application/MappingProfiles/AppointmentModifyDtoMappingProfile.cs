using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Appointments;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class AppointmentModifyDtoMappingProfile : Profile
    {
        public AppointmentModifyDtoMappingProfile()
        {
            CreateMap<AppointmentModifyDto, Modify.Command>();
        }
    }
}
