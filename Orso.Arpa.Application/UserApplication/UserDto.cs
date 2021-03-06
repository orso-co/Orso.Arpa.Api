using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.UserApplication
{
    public class UserDto
    {
        public string UserName { get; set; }
        public IList<string> RoleNames { get; set; } = new List<string>();
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CreatedAt { get; set; }
        public IList<Guid> StakeholderGroupIds { get; set; } = new List<Guid>();
    }

    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.StakeholderGroupIds, opt => opt.MapFrom(src => src.Person.StakeholderGroups.Select(g => g.SectionId)))
                .ForMember(dest => dest.RoleNames, opt => opt.Ignore());
        }
    }
}
