using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using static Orso.Arpa.Domain.Logic.Auth.ChangePassword;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class ChangePasswordCommandValidatorTests
    {
        private Validator _validator;
        private ArpaUserManager _userManager;
        private IUserAccessor _userAccessor;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _validator = new Validator(_userManager, _userAccessor);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Wrong_Current_Password_Is_Supplied()
        {
            _ = _userAccessor.GetCurrentUserAsync(Arg.Any<CancellationToken>()).Returns(Arpa.Tests.Shared.FakeData.FakeUsers.Performer);
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(command => command.CurrentPassword, "WrongPassword");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Correct_Current_Password_Is_Supplied()
        {
            _ = _userAccessor.GetCurrentUserAsync(Arg.Any<CancellationToken>()).Returns(Arpa.Tests.Shared.FakeData.FakeUsers.Performer);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.CurrentPassword, UserSeedData.ValidPassword);
        }
    }
}
