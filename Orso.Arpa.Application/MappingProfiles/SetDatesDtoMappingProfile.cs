using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Appointments;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class SetDatesDtoMappingProfile : Profile
    {
        public SetDatesDtoMappingProfile()
        {
            CreateMap<SetDatesDto, SetDates.Command>();
        }
    }
}
