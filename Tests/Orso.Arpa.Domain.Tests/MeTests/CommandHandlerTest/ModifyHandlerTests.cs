using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.MeTests.CommandHandlerTest
{
    [TestFixture]
    public class ModifyHandlerTests
    {
        private ArpaUserManager _userManager;
        private IUserAccessor _userAccessor;
        private ModifyMyUser.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _handler = new ModifyMyUser.Handler(_userManager, _userAccessor);
        }

        [Test]
        public async Task Should_Modify_User()
        {
            // Arrange
            User user = FakeUsers.Performer;
            _ = _userAccessor.GetCurrentUserAsync(Arg.Any<CancellationToken>()).Returns(user);

            // Act
            Unit result = await _handler.Handle(new ModifyMyUser.Command(), new CancellationToken());

            // Assert
            _ = result.Should().Be(Unit.Value);
        }
    }
}
