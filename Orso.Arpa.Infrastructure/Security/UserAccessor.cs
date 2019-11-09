using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Errors;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public UserAccessor(
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string GetCurrentUserName()
        {
            var username = _httpContextAccessor.HttpContext.User?.Claims?
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                .Value;
            if (username == null)
            {
                throw new RestException("Authorization failed", HttpStatusCode.Unauthorized);
            }
            return username;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var username = GetCurrentUserName();
            User user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new RestException("Authorization failed", HttpStatusCode.Unauthorized);
            }
            return user;
        }

        public IEnumerable<string> GetCurrentUserRoles()
        {
            return _httpContextAccessor.HttpContext.User?.Claims?
                .Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?
                .Select(c => c.Value);
        }
    }
}
