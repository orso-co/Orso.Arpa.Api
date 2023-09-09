using FluentValidation;
using Orso.Arpa.Application.General.Extensions;

namespace Orso.Arpa.Application.SelectValueApplication.Model
{
    public class SelectValueCreateDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class SelectValueCreateDtoValidator : AbstractValidator<SelectValueCreateDto>
    {
        public SelectValueCreateDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .PlaceName(50);

            RuleFor(s => s.Description)
                .RestrictedFreeText(255);
        }
    }
}
