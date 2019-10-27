using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain;

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
                var users = new List<User>
                {
                    Egon,
                    Erna,
                    Fritz
                };

                foreach (User user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }
        }

        public static User Egon
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("6F836C0E-E27D-4363-A67A-9DA92037A589"),
                    UserName = "egon",
                    Email = "egon@test.com"
                };
            }
        }

        public static User Erna
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("DD0AA267-120E-4BD6-B6EB-02814C227E15"),
                    UserName = "erna",
                    Email = "erna@test.com"
                };
            }
        }

        public static User Fritz
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("29CFA973-20D6-4603-91BA-6F4C1F79A6FA"),
                    UserName = "fritz",
                    Email = "fritz@test.com"
                };
            }
        }
    }
}
