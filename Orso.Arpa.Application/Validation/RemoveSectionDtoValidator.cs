using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class RemoveSectionDtoValidator : AbstractValidator<RemoveSectionDto>
    {
        public RemoveSectionDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.SectionId)
                .NotEmpty();
        }
    }
}
