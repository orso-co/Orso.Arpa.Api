using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.UserApplication
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public int RoleLevel { get; set; }
        public string DisplayName { get; set; }
    }

    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.Ignore());
        }
    }
}
