using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Users.Dtos
{
    public class UserProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserProfileDtoMappingProfile : Profile
    {
        public UserProfileDtoMappingProfile()
        {
            CreateMap<User, UserProfileDto>();
        }
    }
}
