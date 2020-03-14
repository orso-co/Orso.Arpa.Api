using FluentValidation;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionCreateDto
    {
        // ToDo: Add properties
        public string Name { get; set; }
    }

    public class SectionCreateDtoValidator : AbstractValidator<SectionCreateDto>
    {
        public SectionCreateDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
