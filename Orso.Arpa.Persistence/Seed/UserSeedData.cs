using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Seed
{
    public static class UserSeedData
    {
        public static IList<User> Users
        {
            get
            {
                return new List<User> {
                    Admin,
                };
            }
        }

        public static User Admin
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("b9ba1467-ad6f-40e5-a0c6-f482393b7feb"),
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
