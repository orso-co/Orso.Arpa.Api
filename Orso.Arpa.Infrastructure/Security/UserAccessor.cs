using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext.User?.Claims?
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                .Value;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var username = GetCurrentUsername();
            if (username != null)
            {
                return await _userManager.FindByNameAsync(username);
            }
            return null;
        }
    }
}
