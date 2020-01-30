using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class SetDatesDtoValidator : AbstractValidator<SetDatesDto>
    {
        public SetDatesDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(c => c.EndTime)
                    .NotNull()
                    .When(c => c.StartTime == null);
            RuleFor(c => c.StartTime)
                .NotNull()
                .When(c => c.EndTime == null);
            RuleFor(d => d.Id)
                .NotEmpty();
        }
    }
}
