using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Application.Auth.Dtos;
using Orso.Arpa.Domain;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class AuthControllerTest : IntegrationTestBase
    {
        [Test]
        public async Task Should_Login()
        {
            // Arrange
            User user = UserSeedData.Orsianer;
            var loginQuery = new Login.Query
            {
                Email = user.Email,
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginQuery));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            TokenDto result = await DeserializeResponseMessageAsync<TokenDto>(responseMessage);

            result.Token.Should().NotBeNullOrEmpty();

            JwtSecurityToken decryptedToken = new JwtSecurityTokenHandler().ReadJwtToken(result.Token);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value.Should().Be(user.UserName);
        }

        [Test]
        public async Task Should_Not_Login_Unregistered_User()
        {
            // Arrange
            var loginQuery = new Login.Query
            {
                Email = "unregistered@test.com",
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginQuery));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Should_Not_Login_Invalid_Password()
        {
            // Arrange
            User user = UserSeedData.Orsianer;
            var loginQuery = new Login.Query
            {
                Email = user.Email,
                Password = "invalidPassword"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginQuery));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Should_Register()
        {
            // Arrange
            var registerCommand = new Register.Command
            {
                Email = "ludmilla@test.com",
                UserName = "ludmilla",
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerCommand));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            TokenDto result = await DeserializeResponseMessageAsync<TokenDto>(responseMessage);

            result.Token.Should().NotBeNullOrEmpty();

            JwtSecurityToken decryptedToken = new JwtSecurityTokenHandler().ReadJwtToken(result.Token);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value.Should().Be(registerCommand.UserName);
        }

        [Test]
        public async Task Should_Not_Register_Existing_Email()
        {
            // Arrange
            var registerCommand = new Register.Command
            {
                Email = UserSeedData.Orsianer.Email,
                UserName = "ludmilla",
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerCommand));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_Not_Register_Existing_UserName()
        {
            // Arrange
            var registerCommand = new Register.Command
            {
                Email = "ludmilla@test.com",
                UserName = UserSeedData.Orsianer.UserName,
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerCommand));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_Change_Password()
        {
            // Arrange
            var command = new ChangePassword.Command
            {
                CurrentPassword = UserSeedData.ValidPassword,
                NewPassword = "NewPa$$w0rd"
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .PutAsync(ApiEndpoints.AuthController.Password(), BuildStringContent(command));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Should_Not_Change_Wrong_Password()
        {
            // Arrange
            var command = new ChangePassword.Command
            {
                CurrentPassword = "WrongPassword",
                NewPassword = "NewPa$$w0rd"
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .PutAsync(ApiEndpoints.AuthController.Password(), BuildStringContent(command));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_Not_Change_Password_Of_Unauthenticated_User()
        {
            // Arrange
            User user = UserSeedData.Orsianer;
            var command = new ChangePassword.Command
            {
                CurrentPassword = UserSeedData.ValidPassword,
                NewPassword = "NewPa$$w0rd"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PutAsync(ApiEndpoints.AuthController.Password(), BuildStringContent(command));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        private static IEnumerable<TestCaseData> SetRoleTestData
        {
            get
            {
                yield return new TestCaseData(UserSeedData.UserWithoutRole, UserSeedData.Orsoadmin, RoleNames.Orsoadmin, HttpStatusCode.OK);
                yield return new TestCaseData(UserSeedData.UserWithoutRole, UserSeedData.Orsonaut, RoleNames.Orsoadmin, HttpStatusCode.Forbidden);
                yield return new TestCaseData(UserSeedData.UserWithoutRole, UserSeedData.Orsianer, RoleNames.Orsoadmin, HttpStatusCode.Forbidden);
                yield return new TestCaseData(UserSeedData.UserWithoutRole, UserSeedData.Orsoadmin, RoleNames.Orsonaut, HttpStatusCode.OK);
                yield return new TestCaseData(UserSeedData.UserWithoutRole, UserSeedData.Orsonaut, RoleNames.Orsonaut, HttpStatusCode.OK);
                yield return new TestCaseData(UserSeedData.UserWithoutRole, UserSeedData.Orsianer, RoleNames.Orsonaut, HttpStatusCode.Forbidden);
                yield return new TestCaseData(UserSeedData.UserWithoutRole, UserSeedData.Orsoadmin, RoleNames.Orsianer, HttpStatusCode.OK);
                yield return new TestCaseData(UserSeedData.UserWithoutRole, UserSeedData.Orsonaut, RoleNames.Orsianer, HttpStatusCode.OK);
                yield return new TestCaseData(UserSeedData.UserWithoutRole, UserSeedData.Orsianer, RoleNames.Orsianer, HttpStatusCode.Forbidden);
                yield return new TestCaseData(UserSeedData.Orsoadmin, UserSeedData.Orsonaut, RoleNames.Orsianer, HttpStatusCode.Forbidden);
                yield return new TestCaseData(UserSeedData.Orsoadmin, UserSeedData.Orsianer, RoleNames.Orsianer, HttpStatusCode.Forbidden);
                yield return new TestCaseData(UserSeedData.Orsoadmin, UserSeedData.Orsoadmin, RoleNames.Orsianer, HttpStatusCode.OK);
            }
        }

        [Test]
        [TestCaseSource(nameof(SetRoleTestData))]
        public async Task Should_Set_Role(User userToEdit, User currentUser, string newRole, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            User user = userToEdit;
            var setRolesCommand = new SetRole.Command
            {
                RoleName = newRole,
                UserName = user.UserName
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(currentUser)
                .PutAsync(ApiEndpoints.AuthController.SetRole(), BuildStringContent(setRolesCommand));

            // Assert
            responseMessage.StatusCode.Should().Be(expectedStatusCode);
        }
    }
}
