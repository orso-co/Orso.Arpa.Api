using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Tests.Shared.Identity
{
    public class FakeSignInManager : SignInManager<User>
    {
        public FakeSignInManager()
                : base(new FakeUserManager(),
                     Substitute.For<IHttpContextAccessor>(),
                     Substitute.For<IUserClaimsPrincipalFactory<User>>(),
                     Substitute.For<IOptions<IdentityOptions>>(),
                     Substitute.For<ILogger<SignInManager<User>>>(),
                     Substitute.For<IAuthenticationSchemeProvider>())
        { }

        public override Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            return Task.FromResult(SignInResult.Success);
        }
    }
}
