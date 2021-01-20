using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using netDumbster.smtp;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class AuthControllerTests : IntegrationTestBase
    {
        private SimpleSmtpServer _fakeSmtpServer;

        [TearDown]
        [SetUp]
        public void SetupAndTearDown()
        {
            if (_fakeSmtpServer != null)
            {
                _fakeSmtpServer.Stop();
                _fakeSmtpServer.Dispose();
            }
        }

        [Test]
        public async Task Should_Login()
        {
            // Arrange
            User user = UserSeedData.Orsoadmin;
            var loginDto = new LoginDto
            {
                UserName = user.UserName,
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            TokenDto result = await DeserializeResponseMessageAsync<TokenDto>(responseMessage);

            result.Token.Should().NotBeNullOrEmpty();

            JwtSecurityToken decryptedToken = new JwtSecurityTokenHandler().ReadJwtToken(result.Token);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value.Should().Be(user.UserName);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value.Should().Be(user.DisplayName);
        }

        [Test]
        public async Task Should_Not_Login_Unregistered_User()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                UserName = "unregistered",
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Should_Not_Login_Invalid_Password()
        {
            // Arrange
            User user = UserSeedData.Orsianer;
            var loginDto = new LoginDto
            {
                UserName = user.UserName,
                Password = "invalidPassword"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Should_Not_Login_LockedOut_User()
        {
            // Arrange
            User user = UserSeedData.LockedOutUser;
            var loginDto = new LoginDto
            {
                UserName = user.UserName,
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Should_Register()
        {
            // Arrange
            var registerDto = new UserRegisterDto
            {
                Email = "ludmilla@test.com",
                UserName = "ludmilla",
                Password = UserSeedData.ValidPassword,
                GivenName = "Ludmilla",
                Surname = "Schneider"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            TokenDto result = await DeserializeResponseMessageAsync<TokenDto>(responseMessage);

            result.Token.Should().NotBeNullOrEmpty();

            JwtSecurityToken decryptedToken = new JwtSecurityTokenHandler().ReadJwtToken(result.Token);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value.Should().Be(registerDto.UserName);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value.Should().Be("Ludmilla Schneider");
        }

        [Test]
        public async Task Should_Not_Register_Existing_Email()
        {
            // Arrange
            var registerDto = new UserRegisterDto
            {
                Email = UserSeedData.Orsianer.Email,
                UserName = "ludmilla",
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_Not_Register_Existing_UserName()
        {
            // Arrange
            var registerDto = new UserRegisterDto
            {
                Email = "ludmilla@test.com",
                UserName = UserSeedData.Orsianer.UserName,
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_Change_Password()
        {
            // Arrange
            var dto = new ChangePasswordDto
            {
                CurrentPassword = UserSeedData.ValidPassword,
                NewPassword = "NewPa$$w0rd"
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .PutAsync(ApiEndpoints.AuthController.Password(), BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task Should_Not_Change_Wrong_Password()
        {
            // Arrange
            var dto = new ChangePasswordDto
            {
                CurrentPassword = "WrongPassword",
                NewPassword = "NewPa$$w0rd"
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .PutAsync(ApiEndpoints.AuthController.Password(), BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_Not_Change_Password_Of_Unauthenticated_User()
        {
            // Arrange
            var dto = new ChangePasswordDto
            {
                CurrentPassword = UserSeedData.ValidPassword,
                NewPassword = "NewPa$$w0rd"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PutAsync(ApiEndpoints.AuthController.Password(), BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        private static IEnumerable<TestCaseData> SetRoleTestData
        {
            get
            {
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Orsoadmin, RoleNames.Orsoadmin, HttpStatusCode.NoContent);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Orsonaut, RoleNames.Orsoadmin, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Orsianer, RoleNames.Orsoadmin, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Orsoadmin, RoleNames.Orsonaut, HttpStatusCode.NoContent);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Orsonaut, RoleNames.Orsonaut, HttpStatusCode.NoContent);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Orsianer, RoleNames.Orsonaut, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Orsoadmin, RoleNames.Orsianer, HttpStatusCode.NoContent);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Orsonaut, RoleNames.Orsianer, HttpStatusCode.NoContent);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Orsianer, RoleNames.Orsianer, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.Orsoadmin, FakeUsers.Orsonaut, RoleNames.Orsianer, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.Orsoadmin, FakeUsers.Orsianer, RoleNames.Orsianer, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.Orsoadmin, FakeUsers.Orsoadmin, RoleNames.Orsianer, HttpStatusCode.NoContent);
            }
        }

        [Test]
        [TestCaseSource(nameof(SetRoleTestData))]
        public async Task Should_Set_Role(User userToEdit, User currentUser, string newRole, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            User user = userToEdit;
            var setRoleDto = new SetRoleDto
            {
                RoleName = newRole,
                UserName = user.UserName
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(currentUser)
                .PutAsync(ApiEndpoints.AuthController.SetRole(), BuildStringContent(setRoleDto));

            // Assert
            responseMessage.StatusCode.Should().Be(expectedStatusCode);
        }

        [Test]
        public async Task Should_Reset_Password()
        {
            // Arrange
            _fakeSmtpServer = Configuration.Configure()
                 .WithPort(25)
                 .Build();

            User user = UserSeedData.Orsianer;
            var forgotPasswordDto = new ForgotPasswordDto
            {
                UserName = user.UserName,
                ClientUri = "http://localhost:4200/auth/resetpassword?token="
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.ForgotPassword(), BuildStringContent(forgotPasswordDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            var resetPasswordToken = _fakeSmtpServer.ReceivedEmail.First().MessageParts.First().BodyData.Split(forgotPasswordDto.ClientUri)[1];

            _fakeSmtpServer.Stop();

            // Arrange
            var resetPasswordDto = new ResetPasswordDto
            {
                UserName = user.UserName,
                Password = UserSeedData.ValidPassword,
                Token = resetPasswordToken
            };

            // Act
            HttpResponseMessage resetResponseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.ResetPassword(), BuildStringContent(resetPasswordDto));

            // Assert
            resetResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
