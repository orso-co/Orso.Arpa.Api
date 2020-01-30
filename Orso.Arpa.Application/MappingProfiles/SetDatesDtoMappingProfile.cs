using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Logic.Appointments;

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
