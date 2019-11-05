using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Seed
{
    public static class RoleSeedData
    {
        public static IList<Role> Roles
        {
            get
            {
                return new List<Role>
                {
                    Orsianer,
                    Orsonaut,
                    Orsoadmin
                };
            }
        }

        public static Role Orsianer
        {
            get
            {
                return new Role
                {
                    Id = Guid.Parse("3828A012-6FC7-4554-85CA-40DE365AC337"),
                    Name = RoleNames.Orsianer
                };
            }
        }

        public static Role Orsonaut
        {
            get
            {
                return new Role
                {
                    Id = Guid.Parse("79A1A749-779F-4B27-81E8-30646C928D86"),
                    Name = RoleNames.Orsonaut
                };
            }
        }

        public static Role Orsoadmin
        {
            get
            {
                return new Role
                {
                    Id = Guid.Parse("B1FA51C8-86A6-4DAD-8BCA-775823C72BA4"),
                    Name = RoleNames.Orsoadmin
                };
            }
        }
    }
}
