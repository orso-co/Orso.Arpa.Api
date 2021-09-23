using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
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
            _arpaContext = Substitute.For<IArpaContext>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UserRegister.Handler(_userManager, new FakeDateTimeProvider(), _arpaContext, _mapper);
        }

        private ArpaUserManager _userManager;
        private UserRegister.Handler _handler;
        private IArpaContext _arpaContext;
        private IMapper _mapper;

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
