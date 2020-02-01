using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Me
{
    public class UserProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.Person.GivenName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Person.Surname));
        }
    }
}
