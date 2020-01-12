using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class AppointmentParticipationDtoMappingProfile : Profile
    {
        public AppointmentParticipationDtoMappingProfile()
        {
            CreateMap<AppointmentParticipation, AppointmentParticipationDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();

            CreateMap<PersonGrouping, AppointmentParticipationListItemDto>()
                .ForMember(dest => dest.Participation, opt => opt.MapFrom(src => src.Participation))
                .ForMember(dest => dest.MusicianProfiles, opt => opt.MapFrom(src => src.Profiles))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person));
        }
    }
}
