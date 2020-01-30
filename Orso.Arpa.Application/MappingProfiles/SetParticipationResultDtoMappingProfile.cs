using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Logic.AppointmentParticipations;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class SetParticipationResultDtoMappingProfile : Profile
    {
        public SetParticipationResultDtoMappingProfile()
        {
            CreateMap<SetParticipationResultDto, SetResult.Command>();
        }
    }
}
