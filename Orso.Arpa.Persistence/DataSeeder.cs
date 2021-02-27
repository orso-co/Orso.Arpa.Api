using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence
{
    public class DataSeeder : IDataSeeder
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ArpaUserManager _arpaUserManager;

        public DataSeeder(RoleManager<Role> roleManager, ArpaUserManager arpaUserManager)
        {
            _roleManager = roleManager;
            _arpaUserManager = arpaUserManager;
        }

        public async Task SeedDataAsync()
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            foreach (Role role in RoleSeedData.Roles)
            {
                if (!(await _roleManager.RoleExistsAsync(role.Name)))
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }

        private async Task SeedUsersAsync()
        {
            foreach (User user in UserSeedData.Users)
            {
                if ((await _arpaUserManager.FindByNameAsync(user.UserName)) is null)
                {
                    await _arpaUserManager.CreateAsync(user, UserSeedData.ValidPassword);
                }
            }

            User admin = await _arpaUserManager.FindByEmailAsync(UserSeedData.Admin.Email);
            if (!(await _arpaUserManager.IsInRoleAsync(admin, RoleNames.Admin)))
            {
                await _arpaUserManager.AddToRoleAsync(admin, RoleNames.Admin);
            }
        }
    }
}
