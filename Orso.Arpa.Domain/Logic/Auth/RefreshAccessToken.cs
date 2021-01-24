using System.Linq;
using System.Security.Authentication;
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
    public static class RefreshAccessToken
    {
        public class Command : IRequest<string>
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

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(
                UserManager<User> userManager,
                IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.Users.Include(x => x.RefreshTokens)
                    .SingleOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == request.RefreshToken && y.UserId == x.Id), cancellationToken);

                if (user == null)
                {
                    throw new ValidationException("No user found for supplied refresh token");
                }

                RefreshToken existingRefreshToken = GetValidRefreshToken(request.RefreshToken, user);

                if (existingRefreshToken == null)
                {
                    throw new AuthenticationException("No valid refresh token available.");
                }

                return await _jwtGenerator.CreateTokensAsync(user, request.RemoteIpAddress);
            }

            private RefreshToken GetValidRefreshToken(string token, User user)
            {
                RefreshToken existingToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);
                return existingToken.IsActive ? existingToken : null;
            }
        }
    }
}
