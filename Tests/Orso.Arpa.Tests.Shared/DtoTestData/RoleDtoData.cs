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
                    RoleName = "Admin",
                    RoleLevel = 3
                };
            }
        }
    }
}
