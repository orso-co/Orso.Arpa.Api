using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class UserRegister
    {
        public class Command : IRequest<Unit>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string ClientUri { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(UserManager<User> userManager)
            {
                RuleFor(c => c.UserName)
                    .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) == null)
                    .WithMessage("Username aleady exists");
                RuleFor(c => c.Email)
                    .MustAsync(async (email, cancellation) => await userManager.FindByEmailAsync(email) == null)
                    .WithMessage("Email aleady exists");
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new User
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    Person = new Person(Guid.NewGuid(), request)
                };

                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                throw new IdentityException("Problem creating user", result.Errors);
            }
        }
    }
}
