using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.UserApplication
{
    public class UserDto
    {
        public string UserName { get; set; }
        public IEnumerable<string> RoleNames { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }

    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleNames, opt => opt.Ignore());
        }
    }
}
