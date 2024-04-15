using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Domain.UserDomain.Commands
{
    public static class RefreshAccessToken
    {
        public class Command : IRequest<bool>
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

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly ArpaUserManager _userManager;
            private readonly IArpaContext _arpaContext;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IDateTimeProvider _dateTimeProvider;
            private readonly ICookieSignIn _cookieSignIn;


            public Handler(
                ArpaUserManager userManager,
                IJwtGenerator jwtGenerator,
                IArpaContext arpaContext,
                ICookieSignIn cookieSignIn,
                IDateTimeProvider dateTimeProvider)
            {
                _userManager = userManager;
                _arpaContext = arpaContext;
                _jwtGenerator = jwtGenerator;
                _cookieSignIn = cookieSignIn;
                _dateTimeProvider = dateTimeProvider;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.Users
                    .Include(x => x.RefreshTokens)
                    .SingleOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == request.RefreshToken && y.UserId == x.Id), cancellationToken);

                if (user == null)
                {
                    throw new ValidationException(new[] { new ValidationFailure(nameof(request.RefreshToken), "No user found for supplied refresh token") });
                }

                RefreshToken existingRefreshToken = GetValidRefreshToken(request.RefreshToken, user);

                if (existingRefreshToken == null)
                {
                    throw new AuthenticationException("No valid refresh token available.");
                }

                if (existingRefreshToken.CreatedByIp != request.RemoteIpAddress)
                {
                    existingRefreshToken.Revoke(new RevokeRefreshToken.Command()
                    {
                        RemoteIpAddress = request.RemoteIpAddress,
                        RefreshToken = request.RefreshToken
                    }, _dateTimeProvider.GetUtcNow());

                    await _arpaContext.SaveChangesAsync(cancellationToken);
                    throw new AuthorizationException("For security reasons, it is not allowed to use this refresh token with a different IP " +
                        "than the one with which the token was created. The refresh token has been revoked. Please log in again.");
                }

                await _jwtGenerator.CreateRefreshTokenAsync(user, request.RemoteIpAddress, cancellationToken);

                Task<Task> signInTask = _cookieSignIn.RefreshSignInAsync(user);

                await signInTask;

                return signInTask.IsCompletedSuccessfully;

            }

            private RefreshToken GetValidRefreshToken(string token, User user)
            {
                RefreshToken existingToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);
                return existingToken?.IsActive(_dateTimeProvider.GetUtcNow()) == true ? existingToken : null;
            }
        }
    }
}
