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
                if (username == null)
                {
                    throw new AuthenticationException("No user name found in the JWT token");
                }
                return username;
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
                return Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Claims?
                   .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?
                   .Value ?? "");
            }
        }

        public Guid PersonId
        {
            get
            {
                return Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Claims?
                   .FirstOrDefault(c => c.Type.Contains("/person_id"))?
                   .Value ?? "");
            }
        }
    }
}
