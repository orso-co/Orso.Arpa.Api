using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class UserProfileDtoData
    {
        public static MyUserProfileDto Performer
        {
            get
            {
                User user = FakeUsers.Performer;
                return new MyUserProfileDto
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Person = PersonDtoData.PerformerForNonStaff
                };
            }
        }
    }
}
