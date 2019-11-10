using System.Collections.Generic;
using Orso.Arpa.Application.Users.Dtos;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class UserDtoData
    {
        public static IList<UserDto> Users
        {
            get
            {
                return new List<UserDto>
                {
                    Orsianer,
                    Orsonaut,
                    Orsoadmin,
                    UserWithoutRole
                };
            }
        }

        public static UserDto Orsianer
        {
            get
            {
                return new UserDto
                {
                    UserName = "orsianer",
                    RoleName = "Orsianer"
                };
            }
        }

        public static UserDto Orsonaut
        {
            get
            {
                return new UserDto
                {
                    UserName = "orsonaut",
                    RoleName = "Orsonaut"
                };
            }
        }

        public static UserDto Orsoadmin
        {
            get
            {
                return new UserDto
                {
                    UserName = "orsoadmin",
                    RoleName = "Orsoadmin"
                };
            }
        }

        public static UserDto UserWithoutRole
        {
            get
            {
                return new UserDto
                {
                    UserName = "withoutrole",
                    RoleName = null
                };
            }
        }
    }
}
