using System;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Persistence.Seed
{
    public static class UserSeedData
    {
        public static User GetInitialAdmin(InitialAdminConfiguration initialAdminConfiguration)
        {
            if (initialAdminConfiguration is null)
            {
                return Admin;
            }

            return new User
            {
                Id = AdminUserId,
                UserName = initialAdminConfiguration.UserName,
                Email = initialAdminConfiguration.Email,
                PersonId = PersonSeedData.AdminPersonId,
                EmailConfirmed = true
            };
        }

        public static Guid AdminUserId => Guid.Parse("b9ba1467-ad6f-40e5-a0c6-f482393b7feb");

        public static User Admin
        {
            get
            {
                return new User
                {
                    Id = AdminUserId,
                    UserName = "admin",
                    Email = "admin@test.com",
                    PersonId = PersonSeedData.Admin.Id,
                    EmailConfirmed = true
                };
            }
        }

        public static string ValidPassword
        {
            get
            {
                return "Pa$$w0rd";
            }
        }
    }
}
