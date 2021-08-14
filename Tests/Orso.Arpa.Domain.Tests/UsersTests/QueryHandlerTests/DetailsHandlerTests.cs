using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Logic.Users;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.UsersTests.QueryHandlerTests
{
    public class DetailsHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new Details.Handler(_userManager);
        }

        private ArpaUserManager _userManager;
        private Details.Handler _handler;

        [Test]
        public async Task Should_Get_User_By_Id()
        {
            // Arrange
            User expectedUser = FakeUsers.LockedOutUser;
            var query = new Details.Query(expectedUser.Id);

            // Act
            User user = await _handler.Handle(query, new CancellationToken());

            // Assert
            user.Should().BeEquivalentTo(expectedUser, opt => opt.Excluding(u => u.ConcurrencyStamp));
        }

        [Test]
        public void Should_Throw_NotFoundException_If_User_Cannot_Be_Found()
        {
            // Arrange
            var query = new Details.Query(Guid.NewGuid());

            // Act
            Func<Task<User>> func = async () => await _handler.Handle(query, new CancellationToken());

            // Assert
            func.Should().ThrowAsync<NotFoundException>().WithMessage("User could not be found.");
        }
    }
}
