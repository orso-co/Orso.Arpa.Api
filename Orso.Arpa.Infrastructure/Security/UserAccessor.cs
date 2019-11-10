using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Errors;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _userManager = userManager;
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
                    throw new RestException("Authentication failed", HttpStatusCode.Unauthorized);
                }
                return username;
            }
        }

        public string DisplayName
        {
            get
            {
                var displayName = _httpContextAccessor?.HttpContext?.User?.Claims?
                   .FirstOrDefault(c => c.Type == ClaimTypes.Name)?
                   .Value;
                if (displayName == null)
                {
                    throw new RestException("Authentication failed", HttpStatusCode.Unauthorized);
                }
                return displayName;
            }
        }

        public async Task<User> GetCurrentUserAsync()
        {
            User user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                throw new RestException("Authentication failed", HttpStatusCode.Unauthorized);
            }
            return user;
        }

        public IEnumerable<string> UserRoles
        {
            get
            {
                return _httpContextAccessor?.HttpContext?.User?.Claims?
                    .Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?
                    .Select(c => c.Value);
            }
        }
    }
}
