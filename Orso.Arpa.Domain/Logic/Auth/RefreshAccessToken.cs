using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class RefreshAccessToken
    {
        public class Command : IRequest<string>
        {
            public string RefreshToken { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(q => q.RefreshToken)
                    .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly UserManager<User> _userManager;
            private readonly IArpaContext _arpaContext;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(
                UserManager<User> userManager,
                IArpaContext arpaContext,
                IJwtGenerator jwtGenerator,
                IHttpContextAccessor httpContextAccessor)
            {
                _userManager = userManager;
                _arpaContext = arpaContext;
                _jwtGenerator = jwtGenerator;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.Users.Include(x => x.RefreshTokens)
                    .SingleOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == request.RefreshToken && y.UserId == x.Id));

                RefreshToken existingRefreshToken = GetValidRefreshToken(request.RefreshToken, user);

                if (existingRefreshToken == null)
                {
                    throw new AuthenticationException("No valid refresh token available.");
                }

                existingRefreshToken.RevokedByIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                existingRefreshToken.RevokedOn = DateTime.UtcNow;
                _arpaContext.Set<RefreshToken>().Update(existingRefreshToken);
                await _arpaContext.SaveChangesAsync(cancellationToken);

                return await _jwtGenerator.CreateTokensAsync(user);
            }

            private RefreshToken GetValidRefreshToken(string token, User user)
            {
                if (user == null)
                {
                    return null;
                }

                RefreshToken existingToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);
                return IsRefreshTokenValid(existingToken) ? existingToken : null;
            }

            private bool IsRefreshTokenValid(RefreshToken existingToken)
            {
                // Is token already revoked, then return false
                if (existingToken.RevokedByIp != null && existingToken.RevokedOn != DateTime.MinValue)
                {
                    return false;
                }

                // Token already expired, then return false
                if (existingToken.ExpiryOn <= DateTime.UtcNow)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
