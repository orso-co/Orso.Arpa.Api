using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;

namespace Orso.Arpa.Application.VenueApplication
{
    public class VenueModifyDto : IdFromRouteDto<VenueModifyBodyDto>
    {
    }

    public class VenueModifyBodyDto
    {
        // ToDo: Add properties
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class VenueModifyDtoValidator : IdFromRouteDtoValidator<VenueModifyDto, VenueModifyBodyDto>
    {
        public VenueModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new VenueModifyBodyDtoValidator());
        }
    }

    public class VenueModifyBodyDtoValidator : AbstractValidator<VenueModifyBodyDto>
    {
        public VenueModifyBodyDtoValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .PlaceName(50);

            RuleFor(v => v.Description)
                .RestrictedFreeText(255);
        }
    }
}
