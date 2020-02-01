using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Roles
{
    public class RoleDto
    {
        public string RoleName { get; set; }
        public int RoleLevel { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RoleLevel, opt => opt.MapFrom(src => src.Level));
        }
    }
}
