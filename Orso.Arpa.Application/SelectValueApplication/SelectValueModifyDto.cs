using FluentValidation;
using Orso.Arpa.Application.General;

namespace Orso.Arpa.Application.SelectValueApplication
{
    public class SelectValueModifyDto : IdFromRouteDto<SelectValueModifyBodyDto>
    {
    }

    public class SelectValueModifyBodyDto
    {
        // ToDo: Add properties
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
                .MaximumLength(50);

            RuleFor(s => s.Description)
                .MaximumLength(255);
        }
    }
}
