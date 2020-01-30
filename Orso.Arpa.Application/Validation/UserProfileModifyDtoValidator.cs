using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class UserProfileModifyDtoValidator : AbstractValidator<UserProfileModifyDto>
    {
        public UserProfileModifyDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(c => c.GivenName)
                .NotEmpty();
            RuleFor(c => c.Surname)
                .NotEmpty();
        }
    }
}
