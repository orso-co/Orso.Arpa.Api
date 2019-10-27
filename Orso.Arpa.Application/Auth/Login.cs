using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Errors;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Application.Auth
{
    public static class Login
    {
        public class Query : IRequest<string>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator(UserManager<User> userManager)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(q => q.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .MustAsync(async (email, cancellation) => (await userManager.FindByEmailAsync(email)) != null)
                    .WithMessage("Unauthorized");
                RuleFor(q => q.Password)
                    .NotEmpty();
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
                User user = await _signInManager.UserManager.FindByEmailAsync(request.Email);

                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    return _jwtGenerator.CreateToken(user);
                };
                throw new RestException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
