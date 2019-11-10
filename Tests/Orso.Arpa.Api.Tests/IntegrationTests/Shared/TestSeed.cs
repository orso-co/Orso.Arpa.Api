using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    internal static class TestSeed
    {

        internal static async Task SeedDataAsync(UserManager<User> userManager)
        {
            await SeedUsers(userManager);
        }

        private static async Task SeedUsers(UserManager<User> userManager)
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
