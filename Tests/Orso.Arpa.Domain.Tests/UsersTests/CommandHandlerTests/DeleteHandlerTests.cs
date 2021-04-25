using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Logic.Users;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.UsersTests.CommandHandlerTests
{
    [TestFixture]
    public class DeleteHandlerTests
    {
        private ArpaUserManager _userManager;
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
            User user = FakeUsers.Performer;

            // Act
            Unit result = await _handler.Handle(new Delete.Command(user.UserName), new CancellationToken());

            // Assert
            result.Should().Be(Unit.Value);
        }

        [Test]
        public void Should_Throw_NotFoundException_If_User_Is_Already_Deleted()
        {
            // Act
            Func<Task<Unit>> func = async () => await _handler.Handle(new Delete.Command("deletedusername"), new CancellationToken());

            // Assert
            func.Should().Throw<NotFoundException>();
        }
    }
}
