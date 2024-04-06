using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Domain.UserDomain.Commands
{
    public static class ConfirmEmail
    {
        public class Command : IRequest
        {
            public string Token { get; set; }
            public string Email { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ArpaUserManager userManager)
            {
                RuleFor(c => c.Email)
                    .MustAsync(async (email, cancellation) => await userManager.FindByEmailAsync(email) != null)
                    .WithErrorCode("404")
                    .WithMessage("User could not be found.");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;

            public Handler(
                ArpaUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByEmailAsync(request.Email);

                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    throw new ValidationException([new ValidationFailure(nameof(request.Email), "The email address is already confirmed")]);
                }

                IdentityResult confirmEmailResult = await _userManager.ConfirmEmailAsync(user, request.Token);

                if (confirmEmailResult.Succeeded)
                {
                    return Unit.Value;
                }

                if (confirmEmailResult.Errors.FirstOrDefault()?.Description.Contains("Invalid token") == true)
                {
                    throw new ValidationException(new ValidationFailure[] { new ValidationFailure(nameof(request.Token), "The supplied token is invalid or has expired") });
                }

                throw new IdentityException("Problem confirming email", confirmEmailResult.Errors);
            }
        }
    }
}
