using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Auth
{
    public static class Register
    {
        public class Command : IRequest<string>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
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
                var user = new User
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    DisplayName = $"{request.GivenName} {request.Surname}",
                    Person = new Person(Guid.NewGuid(), request)
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
