using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Infrastructure.Authentication
{
    public class UserAccessor : TokenAccessor, IUserAccessor
    {
        private readonly ArpaUserManager _userManager;

        public UserAccessor(
            IHttpContextAccessor httpContextAccessor,
            ArpaUserManager userManager) : base(httpContextAccessor)
        {
            _userManager = userManager;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            User user = await _userManager.Users
                .Include(x => x.Person)
                .SingleAsync(x => x.NormalizedUserName == _userManager.NormalizeName(UserName));

            if (user == null)
            {
                throw new AuthenticationException("No user found for the user name provided by the jwt token");
            }
            return user;
        }
    }
}
