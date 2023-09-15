using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

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

            await SeedViewsAndFunctionsAsync();
        }

        private async Task SeedViewsAndFunctionsAsync()
        {
            var sqlDirectory = Path.Combine(AppContext.BaseDirectory, "SqlStatements");
            foreach (var sqlFile in Directory.EnumerateFiles(sqlDirectory, "*.sql").OrderBy(filename => filename))
            {
                var sqlStatement = File.ReadAllText(Path.Combine(sqlDirectory, sqlFile));
                _ = await _arpaContext.ExecuteSqlAsync(sqlStatement);
            }
        }

        private async Task SeedRolesAsync()
        {
            foreach (Role role in RoleSeedData.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role.Name))
                {
                    _ = await _roleManager.CreateAsync(role);
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
                _ = await _arpaContext.Persons.AddAsync(initialAdmin);
                _ = await _arpaContext.SaveChangesAsync(CancellationToken.None);
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
                _ = await _arpaUserManager.CreateAsync(initialAdmin, _seedConfiguration.InitialAdmin?.Password ?? UserSeedData.ValidPassword);
            }

            if (!await _arpaUserManager.IsInRoleAsync(initialAdmin, RoleNames.Admin))
            {
                _ = await _arpaUserManager.AddToRoleAsync(initialAdmin, RoleNames.Admin);
            }
        }
    }
}
