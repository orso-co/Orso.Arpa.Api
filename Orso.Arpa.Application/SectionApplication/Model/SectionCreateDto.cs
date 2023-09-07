using FluentValidation;
using Orso.Arpa.Application.Extensions;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionCreateDto
    {
        public string Name { get; set; }
    }

    public class SectionCreateDtoValidator : AbstractValidator<SectionCreateDto>
    {
        public SectionCreateDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .PlaceName(50);
        }
    }
}
