using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.RoleApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.UrlApplication
{
    public class UrlDto : BaseEntityDto
    {
        public UrlDto(Guid id, string anchorText, string href, List<RoleDto> roles, string createdBy, DateTime createdAt)
        {
            Id = id;
            AnchorText = anchorText;
            Href = href;
            Roles = roles;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
        public UrlDto() { }

        public string Href { get; set; }
        public string AnchorText { get; set; }
        public IList<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }

    public class UrlDtoMappingProfile : Profile
    {
        public UrlDtoMappingProfile()
        {
            CreateMap<Url, UrlDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UrlRoles.Select(ur => ur.Role)))
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
