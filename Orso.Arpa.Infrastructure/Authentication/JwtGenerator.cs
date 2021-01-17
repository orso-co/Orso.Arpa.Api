using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Infrastructure.Authentication
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public JwtGenerator(
            JwtConfiguration jwtConfiguration,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _jwtConfiguration = jwtConfiguration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.TokenKey));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim(ClaimTypes.Name, user.DisplayName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");
            var roleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;

            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, roleName));

            if (!string.IsNullOrEmpty(roleName))
            {
                Role role = await _roleManager.FindByNameAsync(roleName);
                claimsIdentity.AddClaim(new Claim("RoleLevel", role.Level.ToString(), "short"));
            }
            else
            {
                claimsIdentity.AddClaim(new Claim("RoleLevel", "0", "short"));
            }

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.TokenExpirationTimeInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtConfiguration.Issuer,
                Audience = _jwtConfiguration.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDesriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
