using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Logic.Appointments;

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
