using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Domain.UserDomain.Commands
{
    public static class LoginUser
    {
        public class Command : IRequest<bool>
        {
            public string UsernameOrEmail { get; set; }
            public string Password { get; set; }
            public string RemoteIpAddress { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(ArpaUserManager userManager)
            {
                RuleFor(q => q.UsernameOrEmail)
                    .MustAsync(async (userName, cancellation) => await userManager.FindUserByUsernameOrEmailAsync(userName) != null)
                    .WithErrorCode("401")
                    .WithMessage("The system could not log you in. Please enter a valid user name and password");
                RuleFor(q => q.RemoteIpAddress)
                    .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly SignInManager<User> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IdentityConfiguration _identityConfiguration;
            private readonly ICookieSignIn _cookieSignIn;


            public Handler(
                SignInManager<User> signInManager,
                IJwtGenerator jwtGenerator,
                IdentityConfiguration identityConfiguration,
                ArpaUserManager userManager,
                ICookieSignIn cookieSignInService)
            {
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
                _identityConfiguration = identityConfiguration;
                _cookieSignIn = cookieSignInService;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                UserManager<User> userManager = _signInManager.UserManager;
                User user = await userManager.Users
                    .Include(u => u.Person)
                    .SingleOrDefaultAsync(u => request.UsernameOrEmail.Contains('@')
                        ? u.NormalizedEmail == userManager.NormalizeEmail(request.UsernameOrEmail)
                        : u.NormalizedUserName == userManager.NormalizeName(request.UsernameOrEmail), cancellationToken);

                if (!await userManager.IsEmailConfirmedAsync(user))
                {
                    throw new ValidationException(new[] { new ValidationFailure(nameof(request.UsernameOrEmail), "Your email address is not confirmed. Please confirm your email address first") });
                }

                bool IsCookieSignInPossible = await _cookieSignIn.AsyncIsCookieSignInPossible(user, request.Password);

                if (IsCookieSignInPossible)
                {
                    await _jwtGenerator.CreateRefreshTokenAsync(user, request.RemoteIpAddress, cancellationToken);

                    Task<Task> signInTask = _cookieSignIn.AsyncSignInUser(user);
                    await signInTask;

                    return signInTask.IsCompletedSuccessfully;
                }

                SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

                if (result.IsLockedOut)
                {
                    throw new AuthorizationException($"Your account is locked out. Kindly wait for {_identityConfiguration.LockoutExpiryInMinutes} minutes and try again");
                }

                throw new AuthenticationException("The system could not log you in. Please enter a valid user name and password");
            }
        }
    }
}
