using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Application.UserApplication.Model
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public IList<string> RoleNames { get; set; } = new List<string>();
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IList<SectionDto> StakeholderGroups { get; set; } = new List<SectionDto>();
        public UserStatus Status { get; set; }
        public Guid PersonId { get; set; }
    }

    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.StakeholderGroups, opt => opt.MapFrom(src => src.Person.StakeholderGroups.Select(g => g.Section)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom<UserStatusResolver>())
                .ForMember(dest => dest.RoleNames, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name)));
        }
    }

    public class UserStatusResolver : IValueResolver<User, UserDto, UserStatus>
    {
        public UserStatus Resolve(User source, UserDto destination, UserStatus member, ResolutionContext context)
        {
            if (!source.EmailConfirmed)
            {
                return UserStatus.AwaitingEmailConfirmation;
            }
            if (!source.UserRoles.Any())
            {
                return UserStatus.AwaitingRoleAssignment;
            }
            return UserStatus.Active;
        }
    }
}
