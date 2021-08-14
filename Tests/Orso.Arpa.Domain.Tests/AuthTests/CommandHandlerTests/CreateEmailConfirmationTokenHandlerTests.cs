using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class CreateEmailConfirmationTokenHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _emailSender = Substitute.For<IEmailSender>();
            _jwtConfiguration = new JwtConfiguration();
            _clubConfiguration = new ClubConfiguration();
            _handler = new CreateEmailConfirmationToken.Handler(_userManager, _clubConfiguration, _jwtConfiguration, _emailSender);
        }

        private ArpaUserManager _userManager;
        private IEmailSender _emailSender;
        private JwtConfiguration _jwtConfiguration;
        private ClubConfiguration _clubConfiguration;
        private CreateEmailConfirmationToken.Handler _handler;

        [Test]
        public async Task Should_Create_New_Email_Confirmation_Token()
        {
            var command = new CreateEmailConfirmationToken.Command
            {
                UsernameOrEmail = UserTestSeedData.UnconfirmedUser.Email,
                ClientUri = "http://localhost:4200"
            };

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }

        [Test]
        public void Should_Not_Create_New_Email_Confirmation_Token_If_Email_Is_Already_Confirmed()
        {
            var command = new CreateEmailConfirmationToken.Command
            {
                UsernameOrEmail = UserTestSeedData.Performer.Email,
                ClientUri = "http://localhost:4200"
            };

            // Act
            Func<Task<Unit>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            func.Should().ThrowAsync<ValidationException>();
        }
    }
}
