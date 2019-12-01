using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;

namespace Orso.Arpa.Application.Validation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator(UserManager<User> userManager)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(q => q.UserName)
                .NotEmpty()
                .MustAsync(async (userName, cancellation) => await userManager.FindByNameAsync(userName) != null)
                .OnFailure(_ => throw new RestException("Authorization failed", HttpStatusCode.Unauthorized));
            RuleFor(q => q.Password)
                .NotEmpty();
        }
    }
}
