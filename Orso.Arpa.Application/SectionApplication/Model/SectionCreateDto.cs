using FluentValidation;
using Orso.Arpa.Application.General.Extensions;

namespace Orso.Arpa.Application.SectionApplication.Model
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
