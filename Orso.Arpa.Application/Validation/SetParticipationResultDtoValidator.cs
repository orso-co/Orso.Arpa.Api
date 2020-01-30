using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class SetParticipationResultDtoValidator : AbstractValidator<SetParticipationResultDto>
    {
        public SetParticipationResultDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.PersonId)
                .NotEmpty();
            RuleFor(d => d.ResultId)
                .NotEmpty();
        }
    }
}
