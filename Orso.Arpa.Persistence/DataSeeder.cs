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
using Orso.Arpa.Domain.NewsDomain.Model;

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

            if (_seedConfiguration.SeedTestPerformer)
            {
                await SeedTestPerformerPersonAsync();
                await SeedTestPerformerUserAsync();
            }

            if (_seedConfiguration.SeedTestStaff)
            {
                await SeedTestStaffPersonAsync();
                await SeedTestStaffUserAsync();
            }

            if (_seedConfiguration.SeedTestNews)
            {
                await SeedTestNewsAsync();
            }

            await SeedViewsAndFunctionsAsync();
        }

        private async Task SeedViewsAndFunctionsAsync()
        {
            var sqlDirectory = Path.Combine(AppContext.BaseDirectory, "SqlStatements");
            foreach (var sqlFile in Directory.EnumerateFiles(sqlDirectory, "*.sql").OrderBy(filename => filename))
            {
                var sqlStatement = await File.ReadAllTextAsync(Path.Combine(sqlDirectory, sqlFile));
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
            User seedAdmin = UserSeedData.GetInitialAdmin(_seedConfiguration.InitialAdmin);

            if (seedAdmin is null)
            {
                return;
            }

            var existingUser = await _arpaUserManager.FindByNameAsync(seedAdmin.UserName);
            if (existingUser is null)
            {
                _ = await _arpaUserManager.CreateAsync(seedAdmin, _seedConfiguration.InitialAdmin?.Password ?? UserSeedData.ValidPassword);
                existingUser = await _arpaUserManager.FindByNameAsync(seedAdmin.UserName);
            }

            if (existingUser is not null && !await _arpaUserManager.IsInRoleAsync(existingUser, RoleNames.Admin))
            {
                _ = await _arpaUserManager.AddToRoleAsync(existingUser, RoleNames.Admin);
            }
        }

        private async Task SeedTestPerformerPersonAsync()
        {
            Person testPerformer = PersonSeedData.Performer;

            if (!await _arpaContext.EntityExistsAsync<Person>(testPerformer.Id, CancellationToken.None))
            {
                _ = await _arpaContext.Persons.AddAsync(testPerformer);
                _ = await _arpaContext.SaveChangesAsync(CancellationToken.None);
            }
        }

        private async Task SeedTestPerformerUserAsync()
        {
            User seedPerformer = UserSeedData.Performer;

            var existingUser = await _arpaUserManager.FindByNameAsync(seedPerformer.UserName);
            if (existingUser is null)
            {
                _ = await _arpaUserManager.CreateAsync(seedPerformer, UserSeedData.ValidPassword);
                existingUser = await _arpaUserManager.FindByNameAsync(seedPerformer.UserName);
            }

            if (existingUser is not null && !await _arpaUserManager.IsInRoleAsync(existingUser, RoleNames.Performer))
            {
                _ = await _arpaUserManager.AddToRoleAsync(existingUser, RoleNames.Performer);
            }
        }

        private async Task SeedTestStaffPersonAsync()
        {
            Person testStaff = PersonSeedData.Staff;

            if (!await _arpaContext.EntityExistsAsync<Person>(testStaff.Id, CancellationToken.None))
            {
                _ = await _arpaContext.Persons.AddAsync(testStaff);
                _ = await _arpaContext.SaveChangesAsync(CancellationToken.None);
            }
        }

        private async Task SeedTestStaffUserAsync()
        {
            User seedStaff = UserSeedData.Staff;

            var existingUser = await _arpaUserManager.FindByNameAsync(seedStaff.UserName);
            if (existingUser is null)
            {
                _ = await _arpaUserManager.CreateAsync(seedStaff, UserSeedData.ValidPassword);
                existingUser = await _arpaUserManager.FindByNameAsync(seedStaff.UserName);
            }

            if (existingUser is not null && !await _arpaUserManager.IsInRoleAsync(existingUser, RoleNames.Staff))
            {
                _ = await _arpaUserManager.AddToRoleAsync(existingUser, RoleNames.Staff);
            }
        }

        private async Task SeedTestNewsAsync()
        {
            foreach (News news in NewsSeedData.News)
            {
                if (!await _arpaContext.EntityExistsAsync<News>(news.Id, CancellationToken.None))
                {
                    _ = await _arpaContext.News.AddAsync(news);
                }
            }
            _ = await _arpaContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}
