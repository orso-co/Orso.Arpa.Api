using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.UsersTests.CommandHandlerTests
{
    [TestFixture]
    public class ModifyHandlerTests
    {
        private UserManager<User> _userManager;
        private IUserAccessor _userAccessor;
        private IMapper _mapper;
        private Modify.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _mapper = Substitute.For<IMapper>();
            _handler = new Modify.Handler(_userManager, _userAccessor, _mapper);
        }

        [Test]
        public async Task Should_Modify_User()
        {
            // Arrange
            User user = FakeUsers.Performer;
            _userAccessor.GetCurrentUserAsync().Returns(user);
            _mapper.Map(Arg.Any<Modify.Command>(), Arg.Any<User>()).Returns(user);

            // Act
            Unit result = await _handler.Handle(new Modify.Command(), new CancellationToken());

            // Assert
            result.Should().Be(Unit.Value);
        }
    }
}
