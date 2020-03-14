using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class Login
    {
        public class Query : IRequest<string>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator(UserManager<User> userManager)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(q => q.UserName)
                    .MustAsync(async (userName, cancellation) => await userManager.FindByNameAsync(userName) != null)
                    .OnFailure(request => throw new NotFoundException(nameof(User), nameof(Query.UserName), null)); // don't send request with exception as it contains the password
            }
        }

        public class Handler : IRequestHandler<Query, string>
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

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                UserManager<User> userManager = _signInManager.UserManager;
                User user = await userManager.Users
                    .Include(u => u.Person)
                    .SingleOrDefaultAsync(u => u.NormalizedUserName == userManager.NormalizeName(request.UserName));

                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    return await _jwtGenerator.CreateTokenAsync(user);
                }

                throw new AuthenticationException("The system could not log you in. Please enter a valid user name and password");
            }
        }
    }
}
