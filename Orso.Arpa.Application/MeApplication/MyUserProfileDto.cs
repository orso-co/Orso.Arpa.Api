using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyUserProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }

        public string AboutMe { get; set; }
    }

    public class MyUserProfileDtoMappingProfile : Profile
    {
        public MyUserProfileDtoMappingProfile()
        {
            CreateMap<User, MyUserProfileDto>()
                .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.Person.GivenName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Person.Surname))
                .ForMember(dest => dest.AboutMe, opt => opt.MapFrom(src => src.Person.AboutMe));
        }
    }
}
