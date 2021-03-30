using System;
using System.Collections.Generic;
using Orso.Arpa.Application.RoleApplication;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class RoleDtoData
    {
        public static IList<RoleDto> Roles
        {
            get
            {
                return new List<RoleDto>
                {
                    Performer,
                    Staff,
                    Admin,
                };
            }
        }

        public static RoleDto Performer
        {
            get
            {
                return new RoleDto
                {
                    Id = Guid.Parse("3828A012-6FC7-4554-85CA-40DE365AC337"),
                    RoleName = "Performer",
                    RoleLevel = 1
                };
            }
        }

        public static RoleDto Staff
        {
            get
            {
                return new RoleDto
                {
                    Id = Guid.Parse("79A1A749-779F-4B27-81E8-30646C928D86"),
                    RoleName = "Staff",
                    RoleLevel = 2
                };
            }
        }

        public static RoleDto Admin
        {
            get
            {
                return new RoleDto
                {
                    Id = Guid.Parse("B1FA51C8-86A6-4DAD-8BCA-775823C72BA4"),
                    RoleName = "Admin",
                    RoleLevel = 3
                };
            }
        }
    }
}
