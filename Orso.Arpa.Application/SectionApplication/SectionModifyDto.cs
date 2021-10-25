using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionModifyDto : IdFromRouteDto<SectionModifyBodyDto>
    {
    }

    public class SectionModifyBodyDto
    {
        // ToDo: Add properties

        public string Name { get; set; }
    }

    public class SectionModifyDtoValidator : IdFromRouteDtoValidator<SectionModifyDto, SectionModifyBodyDto>
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
               .PlaceName(50);
        }
    }
}
