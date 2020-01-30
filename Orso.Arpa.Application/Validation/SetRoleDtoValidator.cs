using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class SetRoleDtoValidator : AbstractValidator<SetRoleDto>
    {
        public SetRoleDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.UserName)
                .NotEmpty();
        }
    }
}
