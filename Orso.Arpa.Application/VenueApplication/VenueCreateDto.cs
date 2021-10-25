using FluentValidation;
using Orso.Arpa.Application.Extensions;

namespace Orso.Arpa.Application.VenueApplication
{
    public class VenueCreateDto
    {
        // Add properties
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class VenueCreateDtoValidator : AbstractValidator<VenueCreateDto>
    {
        public VenueCreateDtoValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .PlaceName(50);

            RuleFor(v => v.Description)
                .RestrictedFreeText(255);
        }
    }
}
