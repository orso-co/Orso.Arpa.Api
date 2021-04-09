using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.Urls.Create;

namespace Orso.Arpa.Application.UrlApplication
{
    public class UrlCreateDto
    {
        public string Href { get; set; }
        public string AnchorText { get; set; }
        public Guid ProjectId { get; set; }
    }
    public class UrlCreateDtoMappingProfile : Profile
    {
        public UrlCreateDtoMappingProfile()
        {
            CreateMap<UrlCreateDto, Command>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.Href, opt => opt.MapFrom(src => src.Href))
                .ForMember(dest => dest.AnchorText, opt => opt.MapFrom(src => src.AnchorText));
        }
    }

    public class UrlCreateDtoValidator : AbstractValidator<UrlCreateDto>
    {
        public UrlCreateDtoValidator()
        {
            RuleFor(p => p.ProjectId)
               .NotEmpty();

            RuleFor(p => p.Href)
                 .NotEmpty()
                 .ValidUri()
                 .MaximumLength(1000);

            RuleFor(p => p.AnchorText)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
