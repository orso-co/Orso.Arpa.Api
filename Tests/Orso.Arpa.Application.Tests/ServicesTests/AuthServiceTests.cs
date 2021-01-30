using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
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
            _mapper.Map<Login.Command>(loginDto).Returns(new Login.Command
            {
                Password = loginDto.Password,
                UserName = loginDto.UserName
            });
            const string token = "Token";
            _mediator.Send(Arg.Any<Login.Command>()).Returns(token);
            _mapper.Map<TokenDto>(Arg.Any<string>()).Returns(new TokenDto { Token = token });

            // Act
            TokenDto tokenDto = await _authService.LoginAsync(loginDto, "127.0.0.1");

            // Assert
            tokenDto.Token.Should().Be(token);
        }

        [Test]
        public void Should_Register_Async()
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
            _mediator.Send(Arg.Any<Login.Command>()).Returns(token);
            _mapper.Map<TokenDto>(Arg.Any<string>()).Returns(new TokenDto { Token = token });

            // Act
            Func<Task> func = async () => await _authService.RegisterAsync(registerDto);

            // Assert
            func.Should().NotThrow();
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
            _mapper.Map<ChangePassword.Command>(changePasswordDto)
                .Returns(new ChangePassword.Command
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
                RoleName = RoleNames.Staff
            };
            _mapper.Map<SetRole.Command>(setRoleDto)
                .Returns(new SetRole.Command
                {
                    UserName = setRoleDto.UserName,
                    RoleName = setRoleDto.RoleName
                });

            // Act
            Func<Task> func = async () => await _authService.SetRoleAsync(setRoleDto);

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public void Should_Handle_Forgot_Password()
        {
            // Arrange
            var forgotPasswordDto = new ForgotPasswordDto
            {
                UserName = "Username"
            };
            _mapper.Map<ForgotPassword.Command>(forgotPasswordDto)
                .Returns(new ForgotPassword.Command
                {
                    UserName = "Username"
                });

            // Act
            Func<Task> func = async () => await _authService.ForgotPasswordAsync(forgotPasswordDto);

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public void Should_Reset_Password()
        {
            // Arrange
            var resetPasswordDto = new ResetPasswordDto
            {
                UserName = "Username",
                Password = "Password",
                Token = "Token"
            };
            _mapper.Map<ResetPassword.Command>(resetPasswordDto)
                .Returns(new ResetPassword.Command
                {
                    UserName = "Username",
                    Password = "Password",
                    Token = "Token"
                });

            // Act
            Func<Task> func = async () => await _authService.ResetPasswordAsync(resetPasswordDto);

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public void Should_Confirm_Email()
        {
            // Arrange
            var confirmEmailDto = new ConfirmEmailDto
            {
                Email = "test@test.de",
                Token = "Token"
            };
            _mapper.Map<ConfirmEmail.Command>(confirmEmailDto)
                .Returns(new ConfirmEmail.Command
                {
                    Email = "test@test.de",
                    Token = "Token"
                });

            // Act
            Func<Task> func = async () => await _authService.ConfirmEmailAsync(confirmEmailDto);

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public void Should_Create_New_Email_Confirmation_Token()
        {
            // Arrange
            var dto = new CreateEmailConfirmationTokenDto
            {
                Email = "test@test.de",
                ClientUri = "http://localhost:4200"
            };
            _mapper.Map<CreateEmailConfirmationToken.Command>(dto)
                .Returns(new CreateEmailConfirmationToken.Command
                {
                    Email = "test@test.de",
                    ClientUri = "http://localhost:4200"
                });

            // Act
            Func<Task> func = async () => await _authService.CreateNewEmailConfirmationTokenAsync(dto);

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public void Should_Refresh_Token_Async()
        {
            // Arrange

            // Act
            Func<Task> func = async () => await _authService.RefreshAccessTokenAsync("refreshtoken", "127.0.0.1");

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public void Should_Revoke_Token_Async()
        {
            // Arrange

            // Act
            Func<Task> func = async () => await _authService.RevokeRefreshTokenAsync("refreshtoken", "127.0.0.1");

            // Assert
            func.Should().NotThrow();
        }
    }
}
