using Orso.Arpa.Application.Users.Dtos;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class UserProfileDtoData
    {
        public static UserProfileDto Egon
        {
            get
            {
                Domain.User user = UserSeedData.Egon;
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
