using AutoMapper;
using Orso.Arpa.Application.Logic.Me;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class UserProfileDtoMappingProfile : Profile
    {
        public UserProfileDtoMappingProfile()
        {
            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.Person.GivenName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Person.Surname));
        }
    }
}
