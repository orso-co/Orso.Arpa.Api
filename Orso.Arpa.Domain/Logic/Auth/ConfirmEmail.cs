using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Identity;

namespace Orso.Arpa.Domain.Logic.Auth
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
                    .OnFailure(request => throw new NotFoundException(nameof(User), nameof(Command.Email), request));
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
                    throw new ValidationException("One or more validation errors occurred", new[] { new ValidationFailure(nameof(request.Email), "The email address is already confirmed") });
                }

                IdentityResult confirmEmailResult = await _userManager.ConfirmEmailAsync(user, request.Token);

                if (confirmEmailResult.Succeeded)
                {
                    return Unit.Value;
                }

                if (confirmEmailResult.Errors.FirstOrDefault()?.Description.Contains("Invalid token") == true)
                {
                    throw new ValidationException("One or more validation errors occurred", new ValidationFailure[] { new ValidationFailure(nameof(request.Token), "The supplied token is invalid or has expired") });
                }

                throw new IdentityException("Problem confirming email", confirmEmailResult.Errors);
            }
        }
    }
}
