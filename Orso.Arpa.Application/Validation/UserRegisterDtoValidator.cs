using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Validation
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator(UserManager<User> userManager)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.UserName)
                .NotEmpty()
                .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) == null)
                .WithMessage("Username aleady exists");
            RuleFor(c => c.Password)
                .Password();
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, cancellation) => await userManager.FindByEmailAsync(email) == null)
                .WithMessage("Email aleady exists");
            RuleFor(c => c.GivenName)
                .NotEmpty();
            RuleFor(c => c.Surname)
                .NotEmpty();
        }
    }
}
