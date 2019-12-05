using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Auth;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.QueryHandlerTests
{
    public class LoginHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _signInManager = new FakeSignInManager();
            _jwtGenerator = Substitute.For<IJwtGenerator>();
            _handler = new Login.Handler(_signInManager, _jwtGenerator);
        }

        private SignInManager<User> _signInManager;
        private IJwtGenerator _jwtGenerator;
        private Login.Handler _handler;

        [Test]
        public async Task Should_Login_User()
        {
            // Arrange
            string expectedToken = "TestToken";
            User user = FakeUsers.Orsianer;
            var query = new Login.Query { UserName = user.UserName, Password = UserSeedData.ValidPassword };
            _jwtGenerator.CreateTokenAsync(Arg.Any<User>()).Returns(expectedToken);

            // Act
            string token = await _handler.Handle(query, new CancellationToken());

            // Assert
            token.Should().BeEquivalentTo(expectedToken);
        }
    }
}
