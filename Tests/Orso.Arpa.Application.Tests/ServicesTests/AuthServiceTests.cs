using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Logic.Auth;
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
            var loginDto = new LoginDto
            {
                UserName = UserSeedData.Orsianer.UserName,
                Password = UserSeedData.ValidPassword
            };
            _mapper.Map<Login.Query>(loginDto).Returns(new Login.Query { Password = loginDto.Password, UserName = loginDto.UserName });
            const string token = "Token";
            _mediator.Send(Arg.Any<Login.Query>()).Returns(token);
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
            var registerDto = new UserRegisterDto
            {
                Email = UserSeedData.Orsianer.Email,
                Password = UserSeedData.ValidPassword,
                GivenName = "Orsi",
                Surname = "Aner",
                UserName = UserSeedData.Orsianer.UserName
            };
            _mapper.Map<UserRegister.Command>(registerDto).Returns(new UserRegister.Command
            {
                Password = registerDto.Password,
                Email = registerDto.Email,
                GivenName = registerDto.GivenName,
                Surname = registerDto.Surname,
                UserName = registerDto.UserName
            });
            const string token = "Token";
            _mediator.Send(Arg.Any<Login.Query>()).Returns(token);
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
            var changePasswordDto = new ChangePasswordDto
            {
                CurrentPassword = UserSeedData.ValidPassword,
                NewPassword = UserSeedData.ValidPassword + "changed"
            };
            _mapper.Map<ChangePassword.Command>(changePasswordDto).Returns(new ChangePassword.Command
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
            var setRoleDto = new SetRoleDto
            {
                UserName = UserSeedData.Orsianer.UserName,
                RoleName = RoleNames.Orsonaut
            };
            _mapper.Map<SetRole.Command>(setRoleDto).Returns(new SetRole.Command
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
