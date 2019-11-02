using Orso.Arpa.Application.Users.Dtos;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class UserProfileDtoData
    {
        public static UserProfileDto Orsianer
        {
            get
            {
                Domain.User user = UserSeedData.Orsianer;
                return new UserProfileDto
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName
                };
            }

        }
    }
}
