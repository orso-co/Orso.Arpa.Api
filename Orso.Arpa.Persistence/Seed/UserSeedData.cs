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
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
        }

        public static Guid AdminUserId => Guid.Parse("b9ba1467-ad6f-40e5-a0c6-f482393b7feb");
        public static Guid PerformerUserId => Guid.Parse("c8cb2468-be7a-41f6-b1d7-f593494c8fec");
        public static Guid StaffUserId => Guid.Parse("d9dc3579-cf8b-52a7-c2e8-a604505d9afd");

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
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
            }
        }

        public static User Performer
        {
            get
            {
                return new User
                {
                    Id = PerformerUserId,
                    UserName = "performer",
                    Email = "performer@test.com",
                    PersonId = PersonSeedData.PerformerPersonId,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
            }
        }

        public static User Staff
        {
            get
            {
                return new User
                {
                    Id = StaffUserId,
                    UserName = "staff",
                    Email = "staff@test.com",
                    PersonId = PersonSeedData.StaffPersonId,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
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
