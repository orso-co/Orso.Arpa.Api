using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using netDumbster.smtp;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class AuthControllerTests : IntegrationTestBase
    {
        [Test]
        public async Task Should_Login()
        {
            // Arrange
            User user = FakeUsers.Admin;
            var loginDto = new LoginDto
            {
                UsernameOrEmail = user.UserName,
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
            decryptedToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value.Should().Be(user.UserName);
            decryptedToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Name).Value.Should().Be(user.DisplayName);
            decryptedToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value.Should().Be(user.Id.ToString());
            decryptedToken.Claims.First(c => c.Type == "https://localhost:5001/person_id")?.Value.Should().Be(user.PersonId.ToString());
            var refreshToken = GetCookieValueFromResponse(responseMessage, "refreshToken");
            refreshToken.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Should_Not_Login_Unregistered_User()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                UsernameOrEmail = "unregistered",
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("The system could not log you in. Please enter a valid user name and password");
            errorMessage.Status.Should().Be(401);
        }

        [Test]
        public async Task Should_Not_Login_Invalid_Password()
        {
            // Arrange
            User user = UserTestSeedData.Performer;
            var loginDto = new LoginDto
            {
                UsernameOrEmail = user.UserName,
                Password = "invalidPassword"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("The system could not log you in. Please enter a valid user name and password");
            errorMessage.Status.Should().Be(401);
        }

        [Test]
        public async Task Should_Not_Login_LockedOut_User()
        {
            // Arrange
            User user = UserTestSeedData.LockedOutUser;
            var loginDto = new LoginDto
            {
                UsernameOrEmail = user.UserName,
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("Your account is locked out. Kindly wait for 10 minutes and try again");
            errorMessage.Status.Should().Be(403);
        }

        [Test]
        public async Task Should_Not_Login_Unconfirmed_User()
        {
            // Arrange
            User user = UserTestSeedData.UnconfirmedUser;
            var loginDto = new LoginDto
            {
                UsernameOrEmail = user.UserName,
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("One or more validation errors occurred.");
            errorMessage.Status.Should().Be(422);
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "UsernameOrEmail", new[] { "Your email address is not confirmed. Please confirm your email address first" } } });
        }

        [Test]
        public async Task Should_Register_And_Confirm_Email()
        {
            // Arrange
            var registerDto = new UserRegisterDto
            {
                Email = "ludmilla@test.com",
                UserName = "ludmilla",
                Password = UserSeedData.ValidPassword,
                GivenName = "Ludmilla",
                Surname = "Schneider",
                ClientUri = "http://localhost:4200",
                GenderId = SelectValueMappingSeedData.PersonGenderMappings[0].Id
            };
            registerDto.StakeholderGroupIds.Add(SectionSeedData.Volunteers.Id);

            HttpClient client = _unAuthenticatedServer
                .CreateClient();

            // Act
            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);

            // Arrange
            var queryParameters = _fakeSmtpServer.ReceivedEmail.First().MessageParts.First().BodyData.Split("?token=")[1].Split("&email=");
            var confirmEmailToken = queryParameters[0];
            var email = queryParameters[1].Split('"')[0];

            var confirmEmailDto = new ConfirmEmailDto
            {
                Email = email,
                Token = confirmEmailToken
            };

            // Act
            HttpResponseMessage resetResponseMessage = await client
                .PostAsync(ApiEndpoints.AuthController.ConfirmEmail(), BuildStringContent(confirmEmailDto));

            // Assert
            resetResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task Should_Not_Register_Existing_Email()
        {
            // Arrange
            var registerDto = new UserRegisterDto
            {
                Surname = "Nachname",
                GivenName = "Ludmilla",
                Email = UserTestSeedData.Performer.Email,
                UserName = "ludmilla",
                Password = UserSeedData.ValidPassword,
                ClientUri = "http://localhost:4200",
                GenderId = SelectValueMappingSeedData.PersonGenderMappings[0].Id
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("One or more validation errors occurred.");
            errorMessage.Status.Should().Be(422);
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "Email", new[] { "Email aleady exists" } } });
        }

        [Test]
        public async Task Should_Not_Register_Existing_UserName()
        {
            // Arrange
            var registerDto = new UserRegisterDto
            {
                Surname = "Nachname",
                GivenName = "Ludmilla",
                Email = "ludmilla@test.com",
                UserName = UserTestSeedData.Performer.UserName,
                Password = UserSeedData.ValidPassword,
                GenderId = SelectValueMappingSeedData.PersonGenderMappings[0].Id,
                ClientUri = "http://localhost:4200"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("One or more validation errors occurred.");
            errorMessage.Status.Should().Be(422);
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "UserName", new[] { "Username aleady exists" } } });
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
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            SmtpMessage sentEmail = _fakeSmtpServer.ReceivedEmail[0];
            sentEmail.ToAddresses[0].Address.Should().BeEquivalentTo(_performer.Email);
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
            responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("One or more validation errors occurred.");
            errorMessage.Status.Should().Be(422);
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "CurrentPassword", new[] { "Incorrect password supplied" } } });
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
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("Please try to login again");
            errorMessage.Detail.Should().Be("This request requires a valid JWT access token to be provided");
            errorMessage.Status.Should().Be(401);
        }

        private static IEnumerable<TestCaseData> SetRoleTestData
        {
            get
            {
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Admin, new[] { RoleNames.Performer }, HttpStatusCode.NoContent, true);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Staff, new[] { RoleNames.Performer }, HttpStatusCode.NoContent, true);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Performer, new[] { RoleNames.Performer }, HttpStatusCode.Forbidden, false);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Admin, new[] { RoleNames.Staff, RoleNames.Admin }, HttpStatusCode.NoContent, false);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Staff, new[] { RoleNames.Admin }, HttpStatusCode.Forbidden, false);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Performer, new[] { RoleNames.Admin }, HttpStatusCode.Forbidden, false);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Admin, new[] { RoleNames.Staff }, HttpStatusCode.NoContent, false);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Staff, new[] { RoleNames.Staff }, HttpStatusCode.NoContent, false);
                yield return new TestCaseData(FakeUsers.UserWithoutRole, FakeUsers.Performer, new[] { RoleNames.Staff }, HttpStatusCode.Forbidden, false);
                yield return new TestCaseData(FakeUsers.Admin, FakeUsers.Staff, new[] { RoleNames.Performer }, HttpStatusCode.Forbidden, false);
                yield return new TestCaseData(FakeUsers.Admin, FakeUsers.Performer, new[] { RoleNames.Performer }, HttpStatusCode.Forbidden, false);
                yield return new TestCaseData(FakeUsers.Performer, FakeUsers.Admin, Array.Empty<string>(), HttpStatusCode.NoContent, false);
                yield return new TestCaseData(FakeUsers.Admin, FakeUsers.Admin, new[] { RoleNames.Performer }, HttpStatusCode.NoContent, true);
            }
        }

        [Test]
        [TestCaseSource(nameof(SetRoleTestData))]
        public async Task Should_Set_Role(User userToEdit,
                                          User currentUser,
                                          IEnumerable<string> newRole,
                                          HttpStatusCode expectedStatusCode,
                                          bool shouldSendMail)
        {
            // Arrange
            _fakeSmtpServer.ClearReceivedEmail();
            User user = userToEdit;
            var setRoleDto = new SetRoleDto
            {
                RoleNames = newRole,
                Username = user.UserName
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(currentUser)
                .PutAsync(ApiEndpoints.AuthController.SetRole(), BuildStringContent(setRoleDto));

            // Assert
            responseMessage.StatusCode.Should().Be(expectedStatusCode);
            if (shouldSendMail)
            {
                _fakeSmtpServer.ReceivedEmailCount.Should().BeGreaterThan(0);
            }
            else
            {
                _fakeSmtpServer.ReceivedEmailCount.Should().Be(0);
            }
        }

        [Test]
        public async Task Should_Reset_Password()
        {
            // Arrange
            User user = UserTestSeedData.Performer;
            var forgotPasswordDto = new ForgotPasswordDto
            {
                UsernameOrEmail = user.UserName,
                ClientUri = "http://localhost:4200"
            };
            HttpClient client = _unAuthenticatedServer
                .CreateClient();

            // Act

            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.AuthController.ForgotPassword(), BuildStringContent(forgotPasswordDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);

            // Arrange
            var queryParameters = _fakeSmtpServer.ReceivedEmail.First().MessageParts.First().BodyData.Split("?token=")[1].Split("&email=");
            var resetPasswordToken = queryParameters[0];
            var email = queryParameters[1].Split('"')[0];

            var resetPasswordDto = new ResetPasswordDto
            {
                UsernameOrEmail = email,
                Password = UserSeedData.ValidPassword,
                Token = resetPasswordToken
            };
            _fakeSmtpServer.ClearReceivedEmail();

            // Act
            HttpResponseMessage resetResponseMessage = await client
                .PostAsync(ApiEndpoints.AuthController.ResetPassword(), BuildStringContent(resetPasswordDto));

            // Assert
            resetResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            SmtpMessage sentEmail = _fakeSmtpServer.ReceivedEmail[0];
            sentEmail.ToAddresses[0].Address.Should().BeEquivalentTo(_performer.Email);
        }

        [Test]
        public async Task Should_Create_new_Email_Confirmation_Token()
        {
            // Arrange
            var dto = new CreateEmailConfirmationTokenDto
            {
                UsernameOrEmail = UserTestSeedData.UnconfirmedUser.Email,
                ClientUri = "http://localhost:4200"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.CreateNewEmailConfirmationToken(), BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
        }

        [Test]
        public async Task Should_Not_Create_new_Email_Confirmation_Token_If_Email_Is_Already_Confirmed()
        {
            // Arrange
            var dto = new CreateEmailConfirmationTokenDto
            {
                UsernameOrEmail = UserTestSeedData.Performer.Email,
                ClientUri = "http://localhost:4200"
            };

            // Act
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.CreateNewEmailConfirmationToken(), BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(0);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            errorMessage.Title.Should().Be("One or more validation errors occurred.");
            errorMessage.Status.Should().Be(422);
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "UsernameOrEmail", new[] { "The email address is already confirmed" } } });
        }

        [Test]
        public async Task Should_Refresh_Access_Token()
        {
            // Arrange
            HttpClient client = _unAuthenticatedServer
                .CreateClient();
            User user = UserTestSeedData.Staff;
            var loginDto = new LoginDto
            {
                UsernameOrEmail = user.UserName,
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
            User user = UserTestSeedData.Staff;
            var loginDto = new LoginDto
            {
                UsernameOrEmail = user.UserName,
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
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
            HttpResponseMessage refreshMessage = await client.SendAsync(
                CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.AuthController.RefreshToken(), loginResult));
            refreshMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        private static HttpRequestMessage CreateRequestWithCookie(HttpMethod httpMethod, string path, HttpResponseMessage response)
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
