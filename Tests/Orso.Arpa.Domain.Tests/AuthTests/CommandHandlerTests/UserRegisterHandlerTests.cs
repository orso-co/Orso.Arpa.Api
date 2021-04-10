using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class UserRegisterHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new UserRegister.Handler(_userManager, new FakeDateTimeProvider());
        }

        private ArpaUserManager _userManager;
        private UserRegister.Handler _handler;

        [Test]
        public async Task Should_Register_User()
        {
            var command = new UserRegister.Command
            {
                Email = "ludmilla@test.com",
                Password = UserSeedData.ValidPassword,
                UserName = "ludmilla",
                ClientUri = "http://localhost:4200"
            };

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
