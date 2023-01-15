using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Infrastructure.Authentication
{
    public class TokenAccessor : ITokenAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenAccessor(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserName
        {
            get
            {
                var username = _httpContextAccessor?.HttpContext?.User?.Claims?
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                    .Value;
                return username == null
                    ? throw new AuthenticationException("No user name found in the JWT token")
                    : username;
            }
        }

        public string DisplayName
        {
            get
            {
                return _httpContextAccessor?.HttpContext?.User?.Claims?
                   .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?
                   .Value ?? "anonymous";
            }
        }

        public IList<string> UserRoles
        {
            get
            {
                return _httpContextAccessor?.HttpContext?.User?.Claims?
                    .Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?
                    .Select(claim => claim.Value)
                    .ToList();
            }
        }

        public Guid UserId
        {
            get
            {
                var userId = _httpContextAccessor?.HttpContext?.User?.Claims?
                    .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
                return string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userIdAsGuid)
                   ? throw new AuthenticationException("Invalid token")
                   : userIdAsGuid;
            }
        }

        public Guid PersonId
        {
            get
            {
                var personId = _httpContextAccessor?.HttpContext?.User?.Claims?
                   .FirstOrDefault(c => c.Type.Contains("/person_id"))?.Value;
                return string.IsNullOrEmpty(personId) || !Guid.TryParse(personId, out Guid personIdAsGuid)
                   ? throw new AuthenticationException("Invalid token")
                   : personIdAsGuid;
            }
        }
    }
}
