using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class UserRegister
    {
        public class Command : IRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string ClientUri { get; set; }

            public IList<Guid> StakeholderGroupIds { get; set; } = new List<Guid>();
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(ArpaUserManager userManager, IArpaContext context, IStringLocalizer localizer)
            {
                RuleFor(c => c.UserName)
                    .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) == null)
                    .WithMessage("Username aleady exists");
                RuleFor(c => c.Email)
                    .MustAsync(async (email, cancellation) => await userManager.FindByEmailAsync(email) == null)
                    .WithMessage("Email aleady exists");
                RuleForEach(c => c.StakeholderGroupIds)
                    .EntityExists<Command, Section>(context, localizer);
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;
            private readonly IDateTimeProvider _dateTimeProvider;

            public Handler(ArpaUserManager userManager, IDateTimeProvider dateTimeProvider)
            {
                _userManager = userManager;
                _dateTimeProvider = dateTimeProvider;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var person = new Person(Guid.NewGuid(), request);
                foreach (Guid sectionId in request.StakeholderGroupIds)
                {
                    person.StakeholderGroups.Add(new PersonSection(person.Id, sectionId));
                }

                var user = new User
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    Person = person,
                    CreatedAt = _dateTimeProvider.GetUtcNow()
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
