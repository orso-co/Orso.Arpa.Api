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
            public QueryValidator()
            {
                RuleFor(q => q.Email).NotEmpty().EmailAddress();
                RuleFor(q => q.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, string>
        {
            private readonly SignInManager<User> _signInManager;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                IJwtGenerator jwtGenerator)
            {
                _signInManager = signInManager;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                User user = await ValidateAsync(request);

                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    return _jwtGenerator.CreateToken(user);
                };
                throw new RestException(HttpStatusCode.Unauthorized);
            }

            private async Task<User> ValidateAsync(Query request)
            {
                User user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    throw new RestException(HttpStatusCode.Unauthorized);
                }

                return user;
            }
        }
    }
}
