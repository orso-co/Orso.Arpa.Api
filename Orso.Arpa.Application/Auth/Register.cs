using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Validators;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Application.Auth
{
    public static class Register
    {
        public class Command : IRequest<TokenDto>
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(UserManager<User> userManager)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(c => c.Username)
                    .NotEmpty()
                    .MustAsync(async (username, cancellation) => (await userManager.FindByNameAsync(username)) == null)
                    .WithMessage("Username aleady exists");
                RuleFor(c => c.Password)
                    .Password();
                RuleFor(c => c.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .MustAsync(async (email, cancellation) => (await userManager.FindByEmailAsync(email)) == null)
                    .WithMessage("Email aleady exists");

            }
        }

        public class Handler : IRequestHandler<Command, TokenDto>
        {
            private readonly IJwtGenerator _jwtGenerator;
            private readonly UserManager<User> _userManager;

            public Handler(
                UserManager<User> userManager,
                IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
            }

            public async Task<TokenDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new User
                {
                    Email = request.Email,
                    UserName = request.Username
                };

                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new TokenDto
                    {
                        Token = _jwtGenerator.CreateToken(user),
                    };
                }

                throw new Exception($"Problem creating user: {string.Join("; ", result.Errors)}");
            }
        }
    }
}
