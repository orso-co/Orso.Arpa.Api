using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Domain.UserDomain.Commands
{
    public static class ModifyMyUser
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
                _ = RuleFor(c => c.Email)
                    .MustAsync(async (email, cancellation) =>
                    {
                        User user = await userManager.FindByEmailAsync(email);
                        return user == null || userAccessor.UserName == user.UserName;
                    })
                    .WithMessage("Email aleady exists");

                _ = RuleFor(c => c.GenderId)
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
                User existingUser = await _userAccessor.GetCurrentUserAsync(cancellationToken);

                existingUser.Update(request);

                IdentityResult result = await _userManager.UpdateAsync(existingUser);

                return result.Succeeded ? Unit.Value : throw new IdentityException("Problem updating user", result.Errors);
            }
        }
    }
}
