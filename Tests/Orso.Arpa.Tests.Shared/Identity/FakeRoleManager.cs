using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MockQueryable.NSubstitute;
using NSubstitute;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Domain.Roles.Seed;

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

        public override IQueryable<Role> Roles
        {
            get
            {
                IEnumerable<Role> roles = RoleSeedData.Roles;
                return roles.AsQueryable().BuildMock();
            }
        }

        public override Task<bool> RoleExistsAsync(string roleName)
        {
            return Task.FromResult(RoleNames.Roles.Contains(roleName));
        }

        public override Task<Role> FindByNameAsync(string roleName)
        {
            return Task.FromResult(RoleSeedData.Roles.FirstOrDefault(r =>
                r.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
