using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;

namespace Orso.Arpa.Application.SelectValueApplication.Model
{
    public class SelectValueModifyDto : IdFromRouteDto<SelectValueModifyBodyDto>
    {
    }

    public class SelectValueModifyBodyDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class SelectValueModifyDtoValidator : IdFromRouteDtoValidator<SelectValueModifyDto, SelectValueModifyBodyDto>
    {
        public SelectValueModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new SelectValueModifyBodyDtoValidator());
        }
    }

    public class SelectValueModifyBodyDtoValidator : AbstractValidator<SelectValueModifyBodyDto>
    {
        public SelectValueModifyBodyDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .PlaceName(50);

            RuleFor(s => s.Description)
                .RestrictedFreeText(255);
        }
    }
}
