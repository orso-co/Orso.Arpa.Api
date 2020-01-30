using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class RemoveProjectDtoValidator : AbstractValidator<RemoveProjectDto>
    {
        public RemoveProjectDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.ProjectId)
                .NotEmpty();
        }
    }
}
