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
        public Guid Id { get; set; }
    }
    public class UrlCreateDtoMappingProfile : Profile
    {
        public UrlCreateDtoMappingProfile()
        {
            CreateMap<UrlCreateDto, Command>();
        }
    }

    public class UrlCreateDtoValidator : AbstractValidator<UrlCreateDto>
    {
        public UrlCreateDtoValidator()
        {
            RuleFor(p => p.Href)
                .NotEmpty()
                .ValidUri()
                .MaximumLength(1000);

            RuleFor(p => p.AnchorText)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(p => p.Id)
                .NotEmpty();
        }
    }
}
