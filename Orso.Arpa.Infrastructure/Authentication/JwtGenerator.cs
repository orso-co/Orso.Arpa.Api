using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Infrastructure.Authentication
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ArpaUserManager _userManager;
        private readonly IArpaContext _arpaContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTimeProvider _dateTimeProvider;

        public JwtGenerator(
            JwtConfiguration jwtConfiguration,
            ArpaUserManager userManager,
            IArpaContext arpaContext,
            IHttpContextAccessor httpContextAccessor,
            IDateTimeProvider dateTimeProvider)
        {
            _jwtConfiguration = jwtConfiguration;
            _userManager = userManager;
            _arpaContext = arpaContext;
            _httpContextAccessor = httpContextAccessor;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task CreateRefreshTokenAsync(User user, string remoteIpAddress, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = GenerateRefreshToken(user, remoteIpAddress);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiryOn,
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            user.RefreshTokens.Add(refreshToken);
            _arpaContext.Add(refreshToken);

            if (await _arpaContext.SaveChangesAsync(cancellationToken) < 1)
            {
                throw new AffectedRowCountMismatchException(nameof(RefreshToken));
            }
        }

        private RefreshToken GenerateRefreshToken(User user, string ipAddress)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            DateTime now = _dateTimeProvider.GetUtcNow();

            return new RefreshToken
            (
                Convert.ToBase64String(randomBytes),
                now.AddDays(_jwtConfiguration.RefreshTokenExpiryInDays),
                ipAddress,
                user.Id,
                now
            );
        }

        public async Task<ClaimsIdentity> CreateClaimsIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, user.UserName),
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new($"{_jwtConfiguration.Issuer}/person_id", user.PersonId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return claimsIdentity;
        }
    }
}
