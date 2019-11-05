using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    internal static class Seed
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
            }
        }
    }
}
