using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.Identity
{
    public class FakeSignInManager : SignInManager<User>
    {
        private int _failedLoginAttemptCount;

        public FakeSignInManager()
                : base(new FakeUserManager(),
                     Substitute.For<IHttpContextAccessor>(),
                     Substitute.For<IUserClaimsPrincipalFactory<User>>(),
                     Substitute.For<IOptions<IdentityOptions>>(),
                     Substitute.For<ILogger<SignInManager<User>>>(),
                     Substitute.For<IAuthenticationSchemeProvider>(),
                     Substitute.For<IUserConfirmation<User>>())
        { }

        public override Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            if (password == UserSeedData.ValidPassword)
            {
                return Task.FromResult(SignInResult.Success);
            }
            if (_failedLoginAttemptCount >= 3)
            {
                return Task.FromResult(SignInResult.LockedOut);
            }
            _failedLoginAttemptCount++;
            return Task.FromResult(SignInResult.Failed);
        }
    }
}
