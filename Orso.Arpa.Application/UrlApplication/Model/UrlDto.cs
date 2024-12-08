using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.RoleApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Application.UrlApplication.Model
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
        public IList<RoleDto> Roles { get; set; } = [];
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
