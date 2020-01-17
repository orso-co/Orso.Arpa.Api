using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Appointments;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class AppointmentCreateDtoMappingProfile : Profile
    {
        public AppointmentCreateDtoMappingProfile()
        {
            CreateMap<AppointmentCreateDto, Create.Command>();
        }
    }
}
