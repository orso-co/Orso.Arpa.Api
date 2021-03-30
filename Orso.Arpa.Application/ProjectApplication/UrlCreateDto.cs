using FluentValidation;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class UrlCreateDto
    {
        public string Href { get; set; }
        public string AnchorText { get; set; }
    }

    public class UrlCreateDtoValidator : AbstractValidator<UrlCreateDto>
    {
        public UrlCreateDtoValidator()
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
