using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Persistence.Seed
{
    public static class RoleSeedData
    {
        public static IList<Role> Roles
        {
            get
            {
                return new List<Role>
                {
                    Performer,
                    Staff,
                    Admin
                };
            }
        }

        public static Role Performer
        {
            get
            {
                return new Role
                {
                    Id = Guid.Parse("3828A012-6FC7-4554-85CA-40DE365AC337"),
                    Name = RoleNames.Performer,
                    Level = 1
                };
            }
        }

        public static Role Staff
        {
            get
            {
                return new Role
                {
                    Id = Guid.Parse("79A1A749-779F-4B27-81E8-30646C928D86"),
                    Name = RoleNames.Staff,
                    Level = 2
                };
            }
        }

        public static Role Admin
        {
            get
            {
                return new Role
                {
                    Id = Guid.Parse("B1FA51C8-86A6-4DAD-8BCA-775823C72BA4"),
                    Name = RoleNames.Admin,
                    Level = 3
                };
            }
        }
    }
}
