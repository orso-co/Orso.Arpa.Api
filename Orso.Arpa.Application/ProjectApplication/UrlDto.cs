using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class UrlDto : BaseEntityDto
    {
        public string Href { get; set; }
        public string AnchorText { get; set; }
        public IList<Guid> roleIds { get; set; } = new List<Guid>();
    }

    public class UrlDtoMappingProfile : Profile
    {
        public UrlDtoMappingProfile()
        {
            CreateMap<Url, UrlDto>()
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
