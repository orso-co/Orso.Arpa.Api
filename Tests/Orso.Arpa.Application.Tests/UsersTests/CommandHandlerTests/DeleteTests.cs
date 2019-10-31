using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Application.Errors;
using Orso.Arpa.Application.Users;
using Orso.Arpa.Domain;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Application.Tests.UsersTests.CommandHandlerTests
{
    [TestFixture]
    public class DeleteTests
    {
        private UserManager<User> _userManager;
        private Delete.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new Delete.Handler(_userManager);
        }

        [Test]
        public async Task Should_Delete_User()
        {
            // Arrange
            User user = UserSeedData.Egon;

            // Act
            Unit result = await _handler.Handle(new Delete.Command(user.UserName), new CancellationToken());

            // Assert
            result.Should().Be(Unit.Value);
        }

        [Test]
        public void Should_Throw_Rest_Exception_If_User_Is_Already_Deleted()
        {
            // Arrange
            User user = UserSeedData.DeletedUser;

            // Act
            Func<Task<Unit>> func = async () => await _handler.Handle(new Delete.Command(user.UserName), new CancellationToken());

            // Assert
            func.Should().Throw<RestException>();
        }
    }
}
