using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Mail;

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
            private readonly IEmailSender _emailSender;
            private readonly UserManager<User> _userManager;

            public Handler(
                UserManager<User> userManager,
                IEmailSender emailSender)
            {
                _emailSender = emailSender;
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
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    // ToDo: E-Mail Message und Subject definieren. Frontend-Link einfügen

                    var param = new Dictionary<string, string>
                        {
                            { "token", token },
                            { "email", user.Email }
                        };
                    var uri = QueryHelpers.AddQueryString(request.ClientUri, param);
                    var message = new EmailMessage(new string[] { user.Email }, "Confirm email address", uri, false);
                    await _emailSender.SendEmailAsync(message);

                    return Unit.Value;
                }

                throw new IdentityException("Problem creating user", result.Errors);
            }
        }
    }
}
