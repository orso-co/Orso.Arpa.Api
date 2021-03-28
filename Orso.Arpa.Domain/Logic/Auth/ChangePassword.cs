using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class ChangePassword
    {
        public class Command : IRequest
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ArpaUserManager userManager,
                IUserAccessor userAccessor,
                IStringLocalizer<DomainResource>  localizer)
            {
                RuleFor(c => c.CurrentPassword)
                    .MustAsync(async (oldPassword, cancellation) =>
                    {
                        User user = await userAccessor.GetCurrentUserAsync();
                        return await userManager.CheckPasswordAsync(user, oldPassword);
                    })
                    .WithMessage(localizer["Incorrect password supplied"]);
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;
            private readonly IUserAccessor _userAccessor;

            public Handler(
                IUserAccessor userAccessor,
                ArpaUserManager userManager)
            {
                _userManager = userManager;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userAccessor.GetCurrentUserAsync();

                IdentityResult result = await _userManager.ChangePasswordAsync(
                    user,
                    request.CurrentPassword,
                    request.NewPassword);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                throw new IdentityException("Problem changing password", result.Errors);
            }
        }
    }
}
