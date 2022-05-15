using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Me.Modify;

namespace Orso.Arpa.Domain.Tests.MeTests.ValidatorTests
{
    [TestFixture]
    public class UserProfileModifyCommandValidatorTests
    {
        private Validator _validator;
        private ArpaUserManager _userManager;
        private IUserAccessor _userAccessor;
        private IArpaContext _arpaContext;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_userManager, _userAccessor, _arpaContext);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Email_Does_Exist()
        {
            _validator.ShouldHaveValidationErrorForExactAsync(command => command.Email, UserTestSeedData.Performer.Email);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Email, "ludmilla@test.com");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_GivenName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.GivenName, "Ludmilla");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Surname_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Surname, "Schneider");
        }
    }
}
