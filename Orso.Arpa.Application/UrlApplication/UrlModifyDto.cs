using System;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Application.UrlApplication
{
    public class UrlModifyDto : IModifyDto
    {
        public Guid Id { get; set; }
        public string Href { get; set; }
        public string AnchorText { get; set; }
    }

    public class UrlModifyDtoValidator : AbstractValidator<UrlModifyDto>
    {
        public UrlModifyDtoValidator()
        {
            RuleFor(d => d.Id)
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
