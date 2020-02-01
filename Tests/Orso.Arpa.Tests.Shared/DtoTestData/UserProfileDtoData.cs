using Orso.Arpa.Application.Logic.Me;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class UserProfileDtoData
    {
        public static UserProfileDto Orsianer
        {
            get
            {
                User user = FakeUsers.Orsianer;
                Person person = PersonSeedData.Orsianer;
                return new UserProfileDto
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    GivenName = person.GivenName,
                    Surname = person.Surname
                };
            }
        }
    }
}
