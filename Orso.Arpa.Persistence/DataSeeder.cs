using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence
{
    public class DataSeeder : IDataSeeder
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ArpaUserManager _arpaUserManager;
        private readonly SeedConfiguration _seedConfiguration;
        private readonly IArpaContext _arpaContext;

        public DataSeeder(
            RoleManager<Role> roleManager,
            ArpaUserManager arpaUserManager,
            SeedConfiguration seedConfiguration,
            IArpaContext arpaContext)
        {
            _roleManager = roleManager;
            _arpaUserManager = arpaUserManager;
            _seedConfiguration = seedConfiguration;
            _arpaContext = arpaContext;
        }

        public async Task SeedDataAsync()
        {
            await SeedRolesAsync();

            if (_seedConfiguration.SeedInitialAdmin)
            {
                await SeedInitialAdminPersonAsync();
                await SeedInitialAdminUserAsync();
            }
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

        private async Task SeedInitialAdminPersonAsync()
        {
            Person initialAdmin = PersonSeedData.GetInitialAdmin(_seedConfiguration.InitialAdmin);

            if (initialAdmin is null)
            {
                return;
            }

            if (!await _arpaContext.EntityExistsAsync<Person>(initialAdmin.Id, CancellationToken.None))
            {
                await _arpaContext.Persons.AddAsync(initialAdmin);
                await _arpaContext.SaveChangesAsync(CancellationToken.None);
            }
        }

        private async Task SeedInitialAdminUserAsync()
        {
            User initialAdmin = UserSeedData.GetInitialAdmin(_seedConfiguration.InitialAdmin);

            if (initialAdmin is null)
            {
                return;
            }

            if ((await _arpaUserManager.FindByNameAsync(initialAdmin.UserName)) is null)
            {
                await _arpaUserManager.CreateAsync(initialAdmin, _seedConfiguration.InitialAdmin?.Password ?? UserSeedData.ValidPassword);
            }

            if (!(await _arpaUserManager.IsInRoleAsync(initialAdmin, RoleNames.Admin)))
            {
                await _arpaUserManager.AddToRoleAsync(initialAdmin, RoleNames.Admin);
            }
        }
    }
}
