using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.Validation
{
    public class UserProfileModifyDtoValidator : AbstractValidator<UserProfileModifyDto>
    {
        public UserProfileModifyDtoValidator(UserManager<User> userManager, IUserAccessor userAccessor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (dto, email, cancellation) =>
                {
                    User user = await userManager.FindByEmailAsync(email);
                    return user == null || userAccessor.UserName == user.UserName;
                })
                .WithMessage("Email aleady exists");
            RuleFor(c => c.GivenName)
                .NotEmpty();
            RuleFor(c => c.Surname)
                .NotEmpty();
        }
    }
}
