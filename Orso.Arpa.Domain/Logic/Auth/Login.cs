using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Resources.Cultures;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class Login
    {
        public class Command : IRequest<string>
        {
            public string UsernameOrEmail { get; set; }
            public string Password { get; set; }
            public string RemoteIpAddress { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(ArpaUserManager userManager, IStringLocalizer<DomainResource>  localizer)
            {
                RuleFor(q => q.UsernameOrEmail)
                    .MustAsync(async (userName, cancellation) => await userManager.FindUserByUsernameOrEmailAsync(userName) != null)
                    .OnFailure(request => throw new AuthenticationException(localizer["The system could not log you in. Please enter a valid user name and password"]));
                RuleFor(q => q.RemoteIpAddress)
                    .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly SignInManager<User> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IStringLocalizer<DomainResource>  _localizer;

            public Handler(
                SignInManager<User> signInManager,
                IJwtGenerator jwtGenerator,
                IStringLocalizer<DomainResource>  localizer)
            {
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
                _localizer = localizer;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                UserManager<User> userManager = _signInManager.UserManager;
                User user = await userManager.Users
                    .Include(u => u.Person)
                    .SingleOrDefaultAsync(u => (request.UsernameOrEmail.Contains('@'))
                        ? u.NormalizedEmail == userManager.NormalizeEmail(request.UsernameOrEmail)
                        : u.NormalizedUserName == userManager.NormalizeName(request.UsernameOrEmail), cancellationToken);

                if (!await userManager.IsEmailConfirmedAsync(user))
                {
                    throw new ValidationException(new[] { new ValidationFailure(nameof(user.Email), _localizer["Your email address is not confirmed. Please confirm your email address first"]) });
                }

                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

                if (result.Succeeded)
                {
                    return await _jwtGenerator.CreateTokensAsync(user, request.RemoteIpAddress);
                }

                if (result.IsLockedOut)
                {
                    throw new AuthenticationException(_localizer["Your account is locked. Kindly wait for 10 minutes and try again"]);
                }

                throw new AuthenticationException(_localizer["The system could not log you in. Please enter a valid user name and password"]);
            }
        }
    }
}
