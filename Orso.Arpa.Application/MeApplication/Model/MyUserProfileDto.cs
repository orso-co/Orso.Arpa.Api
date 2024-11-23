using AutoMapper;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Application.MeApplication.Model
{
    public class MyUserProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
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
