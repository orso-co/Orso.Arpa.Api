using System.Security.Authentication;
using System.Threading;
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
        private readonly IArpaContext _arpaContext;

        public UserAccessor(
            IHttpContextAccessor httpContextAccessor,
            ArpaUserManager userManager,
            IArpaContext arpaContext) : base(httpContextAccessor)
        {
            _userManager = userManager;
            _arpaContext = arpaContext;
        }

        public async Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            User user = await _userManager.Users
                .Include(x => x.Person)
                .SingleAsync(x => x.NormalizedUserName == _userManager.NormalizeName(UserName), cancellationToken);

            return user ?? throw new AuthenticationException("No user found for the user name provided by the jwt token");
        }

        public async Task<Person> GetCurrentPersonAsync(CancellationToken cancellationToken = default)
        {
            Person person = await _arpaContext.FindAsync<Person>(new object[] { PersonId }, cancellationToken);

            return person ?? throw new AuthenticationException("No person found for the person id provided by the jwt token");
        }
    }
}
