using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Logic.Auth;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ServicesTests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private AuthService _authService;
        private IMapper _mapper;
        private IMediator _mediator;

        [SetUp]
        public void Setup()
        {
            _mapper = Substitute.For<IMapper>();
            _mediator = Substitute.For<IMediator>();
            _authService = new AuthService(_mediator, _mapper);
        }

        [Test]
        public async Task Should_Login_Async()
        {
            // Arrange
            var loginDto = new Logic.Auth.Login.Dto
            {
                UserName = UserSeedData.Orsianer.UserName,
                Password = UserSeedData.ValidPassword
            };
            _mapper.Map<Domain.Logic.Auth.Login.Query>(loginDto).Returns(new Domain.Logic.Auth.Login.Query
            {
                Password = loginDto.Password,
                UserName = loginDto.UserName
            });
            const string token = "Token";
            _mediator.Send(Arg.Any<Domain.Logic.Auth.Login.Query>()).Returns(token);
            _mapper.Map<TokenDto>(Arg.Any<string>()).Returns(new TokenDto { Token = token });

            // Act
            TokenDto tokenDto = await _authService.LoginAsync(loginDto);

            // Assert
            tokenDto.Token.Should().Be(token);
        }

        [Test]
        public async Task Should_Register_Async()
        {
            // Arrange
            var registerDto = new Logic.Auth.UserRegister.Dto
            {
                Email = UserSeedData.Orsianer.Email,
                Password = UserSeedData.ValidPassword,
                GivenName = "Orsi",
                Surname = "Aner",
                UserName = UserSeedData.Orsianer.UserName
            };
            _mapper.Map<Domain.Logic.Auth.UserRegister.Command>(registerDto).Returns(new Domain.Logic.Auth.UserRegister.Command
            {
                Password = registerDto.Password,
                Email = registerDto.Email,
                GivenName = registerDto.GivenName,
                Surname = registerDto.Surname,
                UserName = registerDto.UserName
            });
            const string token = "Token";
            _mediator.Send(Arg.Any<Domain.Logic.Auth.Login.Query>()).Returns(token);
            _mapper.Map<TokenDto>(Arg.Any<string>()).Returns(new TokenDto { Token = token });

            // Act
            TokenDto tokenDto = await _authService.RegisterAsync(registerDto);

            // Assert
            tokenDto.Token.Should().Be(token);
        }

        [Test]
        public void Should_Change_Password()
        {
            // Arrange
            var changePasswordDto = new Logic.Auth.ChangePassword.Dto
            {
                CurrentPassword = UserSeedData.ValidPassword,
                NewPassword = UserSeedData.ValidPassword + "changed"
            };
            _mapper.Map<Domain.Logic.Auth.ChangePassword.Command>(changePasswordDto)
                .Returns(new Domain.Logic.Auth.ChangePassword.Command
                {
                    CurrentPassword = changePasswordDto.CurrentPassword,
                    NewPassword = changePasswordDto.NewPassword
                });

            // Act
            Func<Task> func = async () => await _authService.ChangePasswordAsync(changePasswordDto);

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public void Should_Set_Role()
        {
            // Arrange
            var setRoleDto = new Logic.Auth.SetRole.Dto
            {
                UserName = UserSeedData.Orsianer.UserName,
                RoleName = RoleNames.Orsonaut
            };
            _mapper.Map<Domain.Logic.Auth.SetRole.Command>(setRoleDto)
                .Returns(new Domain.Logic.Auth.SetRole.Command
                {
                    UserName = setRoleDto.UserName,
                    RoleName = setRoleDto.RoleName
                });

            // Act
            Func<Task> func = async () => await _authService.SetRoleAsync(setRoleDto);

            // Assert
            func.Should().NotThrow();
        }
    }
}
