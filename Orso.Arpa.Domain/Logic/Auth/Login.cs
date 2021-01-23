using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class Login
    {
        public class Command : IRequest<string>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(UserManager<User> userManager)
            {
                RuleFor(q => q.UserName)
                    .MustAsync(async (userName, cancellation) => await userManager.FindByNameAsync(userName) != null)
                    .OnFailure(request => throw new AuthenticationException("The system could not log you in. Please enter a valid user name and password"));
            }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly SignInManager<User> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(
                SignInManager<User> signInManager,
                IJwtGenerator jwtGenerator)
            {
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                UserManager<User> userManager = _signInManager.UserManager;
                User user = await userManager.Users
                    .Include(u => u.Person)
                    .SingleOrDefaultAsync(u => u.NormalizedUserName == userManager.NormalizeName(request.UserName));

                if (!await userManager.IsEmailConfirmedAsync(user))
                {
                    throw new AuthenticationException("Your email address is not confirmed. Please confirm your email address first");
                }

                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

                if (result.Succeeded)
                {
                    return await _jwtGenerator.CreateTokensAsync(user);
                }

                if (result.IsLockedOut)
                {
                    throw new AuthenticationException("Your account is locked out. Kindly wait for 10 minutes and try again");
                }

                throw new AuthenticationException("The system could not log you in. Please enter a valid user name and password");
            }
        }
    }
}
