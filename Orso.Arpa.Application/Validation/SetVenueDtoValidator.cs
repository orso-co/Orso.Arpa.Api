using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class SetVenueDtoValidator : AbstractValidator<SetVenueDto>
    {
        public SetVenueDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
        }
    }
}
