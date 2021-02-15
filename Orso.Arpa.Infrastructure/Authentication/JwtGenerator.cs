using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Infrastructure.Authentication
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ArpaUserManager _userManager;
        private readonly IArpaContext _arpaContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtGenerator(
            JwtConfiguration jwtConfiguration,
            ArpaUserManager userManager,
            IArpaContext arpaContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _jwtConfiguration = jwtConfiguration;
            _userManager = userManager;
            _arpaContext = arpaContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> CreateTokensAsync(User user, string remoteIpAddress)
        {
            var accessToken = await CreateAccessTokenAsync(user);
            await CreateRefreshTokenAsync(user, remoteIpAddress);
            return accessToken;
        }

        private async Task<string> CreateAccessTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.TokenKey));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim(ClaimTypes.Name, user.DisplayName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.AccessTokenExpiryInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtConfiguration.Issuer,
                Audience = _jwtConfiguration.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDesriptor);

            return tokenHandler.WriteToken(token);
        }

        private async Task CreateRefreshTokenAsync(User user, string remoteIpAddress)
        {
            RefreshToken refreshToken = GernerateRefreshToken(user, remoteIpAddress);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiryOn,
                IsEssential = true,
                SameSite = SameSiteMode.Strict,
                Secure = true
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            user.RefreshTokens.Add(refreshToken);
            _arpaContext.Add(refreshToken);

            if (!(await _arpaContext.SaveChangesAsync(new CancellationToken()) > 0))
            {
                throw new Exception($"Problem creating {refreshToken.GetType().Name}");
            }
        }

        private RefreshToken GernerateRefreshToken(User user, string ipAddress)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return new RefreshToken
            (
                Convert.ToBase64String(randomBytes),
                DateTime.UtcNow.AddDays(_jwtConfiguration.RefreshTokenExpiryInDays),
                ipAddress,
                user.Id
            );
        }
    }
}
