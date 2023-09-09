using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.MeTests.ValidatorTests
{
    [TestFixture]
    public class UserProfileModifyCommandValidatorTests
    {
        private ModifyMyUser.Validator _validator;
        private ArpaUserManager _userManager;
        private IUserAccessor _userAccessor;
        private IArpaContext _arpaContext;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new ModifyMyUser.Validator(_userManager, _userAccessor, _arpaContext);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Email_Does_Exist()
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Email, UserTestSeedData.Performer.Email);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Email, "ludmilla@test.com");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_GivenName_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.GivenName, "Ludmilla");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Surname_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Surname, "Schneider");
        }
    }
}
