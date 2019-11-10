using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.Validation
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator(
            UserManager<User> userManager,
            IUserAccessor userAccessor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.NewPassword)
                .Password();
            RuleFor(c => c.CurrentPassword)
                .NotEmpty()
                .MustAsync(async (oldPassword, cancellation) =>
                {
                    User user = await userAccessor.GetCurrentUserAsync();
                    return await userManager.CheckPasswordAsync(user, oldPassword);
                })
                .WithMessage("User does not exist or wrong password supplied");
        }
    }
}
