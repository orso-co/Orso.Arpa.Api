using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Orso.Arpa.Domain;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

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

        public override IQueryable<User> Users => UserSeedData.Users.Where(u => !u.Deleted).AsQueryable();

        public override Task<User> FindByEmailAsync(string email)
        {
            return Task.FromResult(UserSeedData.Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase) && !u.Deleted));
        }

        public override Task<User> FindByNameAsync(string userName)
        {
            return Task.FromResult(UserSeedData.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase) && !u.Deleted));
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<bool> CheckPasswordAsync(User user, string password)
        {
            return Task.FromResult(password.Equals(UserSeedData.ValidPassword));
        }

        public override Task<IdentityResult> UpdateAsync(User user)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IList<string>> GetRolesAsync(User user)
        {
            var roleNames = RoleSeedData.Roles.Select(r => r.Name).ToList();
            roleNames.RemoveAll(rn => !rn.Equals(user.UserName, StringComparison.InvariantCultureIgnoreCase));
            return Task.FromResult(roleNames as IList<string>);
        }

        public override Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> RemoveFromRoleAsync(User user, string role)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<bool> IsInRoleAsync(User user, string role)
        {
            if (user.Id == UserSeedData.Orsoadmin.Id && role == RoleNames.Orsoadmin)
            {
                return Task.FromResult(true);
            }
            if (user.Id == UserSeedData.Orsianer.Id && role == RoleNames.Orsianer)
            {
                return Task.FromResult(true);
            }
            if (user.Id == UserSeedData.Orsonaut.Id && role == RoleNames.Orsonaut)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
