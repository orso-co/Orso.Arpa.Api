using System;
using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.RoleApplication
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public short RoleLevel { get; set; }
    }

    public class RoleDtoMappingProfile : Profile
    {
        public RoleDtoMappingProfile()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RoleLevel, opt => opt.MapFrom(src => src.Level));
        }
    }
}
