using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Net.Http.Headers;
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
            User user = UserSeedData.Admin;
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
            var refreshToken = GetCookieValueFromResponse(responseMessage, "refreshToken");
            refreshToken.Should().NotBeNullOrEmpty();
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
            User user = UserSeedData.Performer;
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
        public async Task Should_Not_Login_Unconfirmed_User()
        {
            // Arrange
            User user = UserSeedData.UnconfirmedUser;
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
        public async Task Should_Register_And_Confirm_Email()
        {
            // Arrange
            _fakeSmtpServer = Configuration.Configure()
                 .WithPort(25)
                 .Build();

            var registerDto = new UserRegisterDto
            {
                Email = "ludmilla@test.com",
                UserName = "ludmilla",
                Password = UserSeedData.ValidPassword,
                GivenName = "Ludmilla",
                Surname = "Schneider",
                ClientUri = "http://localhost:4200"
            };
            HttpClient client = _unAuthenticatedServer
                .CreateClient();

            // Act
            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            _fakeSmtpServer.Stop();

            // Arrange
            var queryParameters = _fakeSmtpServer.ReceivedEmail.First().MessageParts.First().BodyData.Split("?token=")[1].Split("&email=");
            var confirmEmailToken = queryParameters[0];
            var email = queryParameters[1];

            var confirmEmailDto = new ConfirmEmailDto
            {
                Email = email,
                Token = confirmEmailToken
            };

            // Act
            HttpResponseMessage resetResponseMessage = await client
                .PostAsync(ApiEndpoints.AuthController.ConfirmEmail(), BuildStringContent(confirmEmailDto));

            // Assert
            resetResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Should_Not_Register_Existing_Email()
        {
            // Arrange
            var registerDto = new UserRegisterDto
            {
                Email = UserSeedData.Performer.Email,
                UserName = "ludmilla",
                Password = UserSeedData.ValidPassword,
                ClientUri = "http://localhost:4200"
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
                UserName = UserSeedData.Performer.UserName,
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
                .AuthenticateWith(_performer)
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
                .AuthenticateWith(_performer)
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
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Admin, RoleNames.Admin, HttpStatusCode.NoContent);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Staff, RoleNames.Admin, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Performer, RoleNames.Admin, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Admin, RoleNames.Staff, HttpStatusCode.NoContent);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Staff, RoleNames.Staff, HttpStatusCode.NoContent);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Performer, RoleNames.Staff, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Admin, RoleNames.Performer, HttpStatusCode.NoContent);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Staff, RoleNames.Performer, HttpStatusCode.NoContent);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Performer, RoleNames.Performer, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.Admin, FakeUsers.Staff, RoleNames.Performer, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.Admin, FakeUsers.Performer, RoleNames.Performer, HttpStatusCode.Forbidden);
                yield return new TestCaseData(FakeUsers.Admin, FakeUsers.Admin, RoleNames.Performer, HttpStatusCode.NoContent);
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

            User user = UserSeedData.Performer;
            var forgotPasswordDto = new ForgotPasswordDto
            {
                UserName = user.UserName,
                ClientUri = "http://localhost:4200"
            };
            HttpClient client = _unAuthenticatedServer
                .CreateClient();

            // Act

            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.AuthController.ForgotPassword(), BuildStringContent(forgotPasswordDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            _fakeSmtpServer.Stop();

            // Arrange
            var resetPasswordToken = _fakeSmtpServer.ReceivedEmail.First().MessageParts.First().BodyData.Split("?token=")[1];
            var resetPasswordDto = new ResetPasswordDto
            {
                UserName = user.UserName,
                Password = UserSeedData.ValidPassword,
                Token = resetPasswordToken
            };

            // Act
            HttpResponseMessage resetResponseMessage = await client
                .PostAsync(ApiEndpoints.AuthController.ResetPassword(), BuildStringContent(resetPasswordDto));

            // Assert
            resetResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Should_Create_new_Email_Confirmation_Token()
        {
            // Arrange
            _fakeSmtpServer = Configuration.Configure()
                 .WithPort(25)
                 .Build();
            var dto = new CreateEmailConfirmationTokenDto
            {
                Email = UserSeedData.UnconfirmedUser.Email,
                ClientUri = "http://localhost:4200"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.CreateNewEmailConfirmationToken(), BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            _fakeSmtpServer.Stop();
        }

        [Test]
        public async Task Should_Not_Create_new_Email_Confirmation_Token_If_Email_Is_Already_Confirmed()
        {
            // Arrange
            _fakeSmtpServer = Configuration.Configure()
                 .WithPort(25)
                 .Build();
            var dto = new CreateEmailConfirmationTokenDto
            {
                Email = UserSeedData.Performer.Email,
                ClientUri = "http://localhost:4200"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.CreateNewEmailConfirmationToken(), BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(0);
            _fakeSmtpServer.Stop();
        }

        [Test]
        public async Task Should_Refresh_Access_Token()
        {
            // Arrange
            HttpClient client = _unAuthenticatedServer
                .CreateClient();
            User user = UserSeedData.Performer;
            var loginDto = new LoginDto
            {
                UserName = user.UserName,
                Password = UserSeedData.ValidPassword
            };

            HttpResponseMessage loginResult = await client
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));
            var firstRefreshToken = GetCookieValueFromResponse(loginResult, "refreshToken");
            TokenDto firstAccessToken = await DeserializeResponseMessageAsync<TokenDto>(loginResult);

            Thread.Sleep(1000); // To ensure a significant difference between the access tokens

            // Act
            HttpResponseMessage responseMessage = await client.SendAsync(
                CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.AuthController.RefreshToken(), loginResult));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            TokenDto result = await DeserializeResponseMessageAsync<TokenDto>(responseMessage);

            result.Token.Should().NotBeNullOrEmpty();
            result.Token.Should().NotBeEquivalentTo(firstAccessToken.Token);

            var refreshToken = GetCookieValueFromResponse(responseMessage, "refreshToken");
            refreshToken.Should().NotBeNullOrEmpty();
            refreshToken.Should().NotBeEquivalentTo(firstRefreshToken);
        }

        [Test]
        public async Task Should_Logout()
        {
            // Arrange
            HttpClient client = _unAuthenticatedServer
                .CreateClient();
            User user = UserSeedData.Performer;
            var loginDto = new LoginDto
            {
                UserName = user.UserName,
                Password = UserSeedData.ValidPassword
            };

            HttpResponseMessage loginResult = await client
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));
            TokenDto tokenDto = await DeserializeResponseMessageAsync<TokenDto>(loginResult);

            // Act
            HttpRequestMessage request =
                CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.AuthController.Logout(), loginResult);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenDto.Token);
            HttpResponseMessage responseMessage = await client.SendAsync(request);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            HttpResponseMessage refreshMessage = await client.SendAsync(
                CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.AuthController.RefreshToken(), loginResult));
            refreshMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        private HttpRequestMessage CreateRequestWithCookie(HttpMethod httpMethod, string path, HttpResponseMessage response)
        {
            var request = new HttpRequestMessage(httpMethod, path);
            if (response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                SetCookieHeaderValue cookie = SetCookieHeaderValue.ParseList(values.ToList()).First();
                request.Headers.Add("Cookie", new CookieHeaderValue(cookie.Name, cookie.Value).ToString());
            }

            return request;
        }
    }
}
