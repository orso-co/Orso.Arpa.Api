using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Orso.Arpa.Domain;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Tests.Shared.Identity
{
    public class FakeRoleManager : RoleManager<Role>
    {
        public FakeRoleManager()
                : base(
                     Substitute.For<IRoleStore<Role>>(),
                     Substitute.For<IEnumerable<IRoleValidator<Role>>>(),
                     Substitute.For<ILookupNormalizer>(),
                     Substitute.For<IdentityErrorDescriber>(),
                     Substitute.For<ILogger<RoleManager<Role>>>())
        { }

        public override Task<bool> RoleExistsAsync(string roleName)
        {
            return Task.FromResult(RoleNames.Roles.Contains(roleName));
        }
    }
}
