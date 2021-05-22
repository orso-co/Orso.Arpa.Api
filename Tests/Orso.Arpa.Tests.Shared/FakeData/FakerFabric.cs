using Bogus;
using Orso.Arpa.Application.MeApplication;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakerFabric
    {
        public static Faker<MyUserProfileDto> UesrProfileDtoFaker
        {
            get
            {
                return new Faker<MyUserProfileDto>()
                    .RuleFor(u => u.GivenName, (f, u) => f.Name.FirstName())
                    .RuleFor(u => u.Surname, (f, u) => f.Name.LastName())
                    .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.GivenName, u.Surname))
                    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.GivenName, u.Surname))
                    .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber());
            }
        }
    }
}
