using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.UsersTests.CommandHandlerTests
{
    [TestFixture]
    public class DeleteHandlerTests
    {
        private ArpaUserManager _userManager;
        private DeleteUser.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new DeleteUser.Handler(_userManager);
        }

        [Test]
        public async Task Should_Delete_User()
        {
            // Arrange
            User user = FakeUsers.Performer;

            // Act
            Unit result = await _handler.Handle(new DeleteUser.Command(user.UserName), new CancellationToken());

            // Assert
            result.Should().Be(Unit.Value);
        }

        [Test]
        public void Should_Throw_NotFoundException_If_User_Is_Already_Deleted()
        {
            // Act
            Func<Task<Unit>> func = async () => await _handler.Handle(new DeleteUser.Command("deletedusername"), new CancellationToken());

            // Assert
            func.Should().ThrowAsync<NotFoundException>();
        }
    }
}
