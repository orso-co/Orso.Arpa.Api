using System;
using FluentValidation;
using Orso.Arpa.Application.Extensions;

namespace Orso.Arpa.Application.UrlApplication
{
    public class UrlCreateDto
    {
        public string Href { get; set; }
        public string AnchorText { get; set; }
        public Guid ProjectId { get; set; }
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

            RuleFor(p => p.ProjectId)
                .NotEmpty();
        }
    }
}
