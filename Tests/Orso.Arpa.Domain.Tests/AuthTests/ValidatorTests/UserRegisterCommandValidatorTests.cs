using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class UserRegisterCommandValidatorTests
    {
        private RegisterUser.Validator _validator;
        private ArpaUserManager _userManager;
        private IArpaContext _context;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _context = Substitute.For<IArpaContext>();
            _validator = new RegisterUser.Validator(_userManager, _context);
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
        public async Task Should_Not_Have_Validation_Error_If_Valid_Password_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Password, UserSeedData.ValidPassword);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_UserName_Does_Exist()
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.UserName, UserTestSeedData.Performer.UserName);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.UserName, "ludmilla");
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
