using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class RevokeRefreshToken
    {
        public class Command : IRequest<Unit>
        {
            public string RefreshToken { get; set; }
            public string RemoteIpAddress { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(q => q.RefreshToken)
                    .NotEmpty();
                RuleFor(q => q.RemoteIpAddress)
                    .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly UserManager<User> _userManager;
            private readonly IArpaContext _arpaContext;

            public Handler(UserManager<User> userManager, IArpaContext arpaContext)
            {
                _userManager = userManager;
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.Users.Include(x => x.RefreshTokens)
                    .SingleOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == request.RefreshToken && y.UserId == x.Id), cancellationToken);

                if (user == null)
                {
                    throw new ValidationException("No user found for supplied refresh token");
                }

                RefreshToken existingToken = user.RefreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken);

                existingToken.Revoke(request);

                _arpaContext.Set<RefreshToken>().Update(existingToken);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception($"Problem updating {existingToken.GetType().Name}");
            }
        }
    }
}
