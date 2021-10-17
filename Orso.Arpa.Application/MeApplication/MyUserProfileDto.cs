using AutoMapper;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyUserProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public PersonDto Person { get; set; }
    }

    public class MyUserProfileDtoMappingProfile : Profile
    {
        public MyUserProfileDtoMappingProfile()
        {
            CreateMap<User, MyUserProfileDto>();
        }
    }
}
