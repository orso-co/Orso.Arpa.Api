using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class Modify
    {
        public class Command : IRequest
        {
            public string Email { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string AboutMe { get; set; }
            public Guid GenderId { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string Birthplace { get; set; }
            public string BirthName { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(ArpaUserManager userManager, IUserAccessor userAccessor, IArpaContext arpaContext)
            {
                RuleFor(c => c.Email)
                    .MustAsync(async (email, cancellation) =>
                    {
                        User user = await userManager.FindByEmailAsync(email);
                        return user == null || userAccessor.UserName == user.UserName;
                    })
                    .WithMessage("Email aleady exists");

                RuleFor(c => c.GenderId)
                    .SelectValueMapping<Command, Person>(arpaContext, p => p.Gender);
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;
            private readonly IUserAccessor _userAccessor;

            public Handler(
                ArpaUserManager userManager,
                IUserAccessor userAccessor)
            {
                _userManager = userManager;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User existingUser = await _userAccessor.GetCurrentUserAsync();

                existingUser.Update(request);

                IdentityResult result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating user");
            }
        }
    }
}
