using System.Collections.Generic;
using Orso.Arpa.Application.Dtos;

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
                    Orsianer,
                    Orsonaut,
                    Orsoadmin,
                };
            }
        }

        public static RoleDto Orsianer
        {
            get
            {
                return new RoleDto
                {
                    RoleName = "Orsianer",
                    RoleLevel = 1
                };
            }
        }

        public static RoleDto Orsonaut
        {
            get
            {
                return new RoleDto
                {
                    RoleName = "Orsonaut",
                    RoleLevel = 2
                };
            }
        }

        public static RoleDto Orsoadmin
        {
            get
            {
                return new RoleDto
                {
                    RoleName = "Orsoadmin",
                    RoleLevel = 3
                };
            }
        }
    }
}
