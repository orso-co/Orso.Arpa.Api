using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Users
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public int RoleLevel { get; set; }
        public string DisplayName { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.Ignore());
        }
    }
}
