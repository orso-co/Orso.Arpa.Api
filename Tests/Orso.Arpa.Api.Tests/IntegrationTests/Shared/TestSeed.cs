using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    internal static class TestSeed
    {
        internal static async Task SeedDataAsync(UserManager<User> userManager, IRepository repository)
        {
            await SeedPersonsAsync(repository);
            await SeedUsersAsync(userManager);
        }

        private static async Task SeedPersonsAsync(IRepository repository)
        {
            foreach (Person person in PersonSeedData.Persons)
            {
                await repository.AddAsync(person);
            }
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                foreach (User user in UserSeedData.Users)
                {
                    await userManager.CreateAsync(user, UserSeedData.ValidPassword);
                }

                User orsianer = await userManager.FindByEmailAsync(UserSeedData.Orsianer.Email);
                await userManager.AddToRoleAsync(orsianer, RoleNames.Orsianer);

                User orsonaut = await userManager.FindByEmailAsync(UserSeedData.Orsonaut.Email);
                await userManager.AddToRoleAsync(orsonaut, RoleNames.Orsonaut);

                User admin = await userManager.FindByEmailAsync(UserSeedData.Orsoadmin.Email);
                await userManager.AddToRoleAsync(admin, RoleNames.Orsoadmin);
            }
        }
    }
}
