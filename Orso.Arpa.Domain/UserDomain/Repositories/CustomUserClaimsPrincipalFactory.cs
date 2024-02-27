using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.UserDomain.Repositories
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public CustomUserClaimsPrincipalFactory(
        UserManager<User> userManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, optionsAccessor)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            ClaimsIdentity claims = await base.GenerateClaimsAsync(user);
            claims.AddClaim(new Claim("nameid", user.UserName));
            claims.AddClaim(new Claim("name", user.DisplayName));
            claims.AddClaim(new Claim("sub", user.Id.ToString()));
            return claims;
        }
    }
}