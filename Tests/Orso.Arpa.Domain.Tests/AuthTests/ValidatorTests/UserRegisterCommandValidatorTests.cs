using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Auth.UserRegister;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class UserRegisterCommandValidatorTests
    {
        private Validator _validator;
        private ArpaUserManager _userManager;
        private IArpaContext _context;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _context = Substitute.For<IArpaContext>();
            _validator = new Validator(_userManager, _context);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Email_Does_Exist()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Email, UserTestSeedData.Performer.Email);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Email, "ludmilla@test.com");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Password_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Password, UserSeedData.ValidPassword);
        }

        [Test]
        public void Should_Have_Validation_Error_If_UserName_Does_Exist()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.UserName, UserTestSeedData.Performer.UserName);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.UserName, "ludmilla");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_GivenName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.GivenName, "Ludmilla");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Surname_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Surname, "Schneider");
        }
    }
}
