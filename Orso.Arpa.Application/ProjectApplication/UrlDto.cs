using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.RoleApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class UrlDto : BaseEntityDto
    {
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

    public class UrlDtoValidator : AbstractValidator<UrlDto>
    {
        public UrlDtoValidator()
        {
            RuleFor(p => p.Href)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(p => p.AnchorText)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
