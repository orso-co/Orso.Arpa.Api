using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.MeTests.CommandHandlerTest
{
    [TestFixture]
    public class ModifyHandlerTests
    {
        private ArpaUserManager _userManager;
        private IUserAccessor _userAccessor;
        private Modify.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _handler = new Modify.Handler(_userManager, _userAccessor);
        }

        [Test]
        public async Task Should_Modify_User()
        {
            // Arrange
            User user = FakeUsers.Performer;
            _ = _userAccessor.GetCurrentUserAsync(Arg.Any<CancellationToken>()).Returns(user);

            // Act
            Unit result = await _handler.Handle(new Modify.Command(), new CancellationToken());

            // Assert
            _ = result.Should().Be(Unit.Value);
        }
    }
}
