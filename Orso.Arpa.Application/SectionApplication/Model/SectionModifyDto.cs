using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;

namespace Orso.Arpa.Application.SectionApplication.Model
{
    public class SectionModifyDto : IdFromRouteDto<SectionModifyBodyDto>
    {
    }

    public class SectionModifyBodyDto
    {
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
