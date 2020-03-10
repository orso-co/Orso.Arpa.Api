using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Infrastructure.Authentication
{
    public class UserAccessor : TokenAccessor, IUserAccessor
    {
        private readonly UserManager<User> _userManager;

        public UserAccessor(
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager) : base(httpContextAccessor)
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
                throw new RestException("User is not authenticated", HttpStatusCode.Unauthorized);
            }
            return user;
        }
    }
}
