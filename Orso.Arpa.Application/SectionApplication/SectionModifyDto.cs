using FluentValidation;
using Orso.Arpa.Application.General;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionModifyDto : BaseModifyDto<SectionModifyBodyDto>
    {
    }

    public class SectionModifyBodyDto
    {
        // ToDo: Add properties

        public string Name { get; set; }
    }

    public class SectionModifyDtoValidator : BaseModifyDtoValidator<SectionModifyDto, SectionModifyBodyDto>
    {
        public SectionModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new SectionModifyBodyDtoValidator());
        }
    }

    public class SectionModifyBodyDtoValidator : AbstractValidator<SectionModifyBodyDto>
    {
        public SectionModifyBodyDtoValidator()
        {
            RuleFor(s => s.Name)
               .NotEmpty()
               .MaximumLength(50);
        }
    }
}
