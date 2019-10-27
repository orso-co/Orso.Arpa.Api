using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Orso.Arpa.Domain;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Tests.Shared.Identity
{
    public class FakeUserManager : UserManager<User>
    {
        public FakeUserManager()
            : base(
              Substitute.For<IUserStore<User>>(),
              Substitute.For<IOptions<IdentityOptions>>(),
              Substitute.For<IPasswordHasher<User>>(),
              new IUserValidator<User>[0],
              new IPasswordValidator<User>[0],
              Substitute.For<ILookupNormalizer>(),
              Substitute.For<IdentityErrorDescriber>(),
              Substitute.For<IServiceProvider>(),
              Substitute.For<ILogger<UserManager<User>>>())
        { }

        public override Task<User> FindByEmailAsync(string email)
        {
            return Task.FromResult(UserSeedData.Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
