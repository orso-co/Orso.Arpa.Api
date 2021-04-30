using System;
using AutoMapper;
using Orso.Arpa.Application.Tranlation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Translation;

namespace Orso.Arpa.Application.RoleApplication
{
    public class RoleDto
    {
        [Translate]
        public Guid Id { get; set; }
        [Translate(nameof(RoleDto))]
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