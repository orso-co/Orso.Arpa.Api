using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class UserRegister
    {
        public class Command : IRequest<string>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(UserManager<User> userManager)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(c => c.UserName)
                    .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) == null)
                    .WithMessage("Username aleady exists");
                RuleFor(c => c.Email)
                    .MustAsync(async (email, cancellation) => await userManager.FindByEmailAsync(email) == null)
                    .WithMessage("Email aleady exists");
            }
        }

        public class Handler : IRequestHandler<Command, string>
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

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var person = new Person(Guid.NewGuid(), request);
                person.Create(request.UserName);

                var user = new User
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    Person = person
                };

                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return await _jwtGenerator.CreateTokenAsync(user);
                }

                throw new IdentityException("Problem creating user", result.Errors);
            }
        }
    }
}
