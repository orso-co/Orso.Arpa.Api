using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Users.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string DisplayName { get; set; }

        public class UserDtoMappingProfile : Profile
        {
            public UserDtoMappingProfile()
            {
                CreateMap<User, UserDto>();
            }
        }
    }
}
