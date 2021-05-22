using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class UserProfileDtoData
    {
        public static MyUserProfileDto Performer
        {
            get
            {
                User user = FakeUsers.Performer;
                Person person = PersonTestSeedData.Performer;
                return new MyUserProfileDto
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
