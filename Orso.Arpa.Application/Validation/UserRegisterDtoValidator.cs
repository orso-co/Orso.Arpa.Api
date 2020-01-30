using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.UserName)
                .NotEmpty();
            RuleFor(c => c.Password)
                .Password();
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
