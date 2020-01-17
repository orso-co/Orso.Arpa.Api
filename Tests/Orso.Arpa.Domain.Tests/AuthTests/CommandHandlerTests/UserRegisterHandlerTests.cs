using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class UserRegisterHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _jwtGenerator = Substitute.For<IJwtGenerator>();
            _handler = new Auth.UserRegister.Handler(_userManager, _jwtGenerator);
        }

        private UserManager<User> _userManager;
        private IJwtGenerator _jwtGenerator;
        private Auth.UserRegister.Handler _handler;

        [Test]
        public async Task Should_Register_User()
        {
            // Arrange
            const string expectedToken = "TestToken";
            var command = new Auth.UserRegister.Command
            {
                Email = "ludmilla@test.com",
                Password = UserSeedData.ValidPassword,
                UserName = "ludmilla"
            };
            _jwtGenerator.CreateTokenAsync(Arg.Any<User>()).Returns(expectedToken);

            // Act
            string token = await _handler.Handle(command, new CancellationToken());

            // Assert
            token.Should().BeEquivalentTo(expectedToken);
        }
    }
}
