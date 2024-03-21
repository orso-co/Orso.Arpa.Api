using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using netDumbster.smtp;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class AuthControllerTests : IntegrationTestBase
    {
        private IHttpContextAccessor _httpContextAccessor;

        [SetUp]
        public void Setup()
        {
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        }

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
            HttpResponseMessage loginResponse = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            var refreshToken = GetCookieValueFromResponse(loginResponse, "refreshToken");
            var sessionCookie = GetCookieValueFromResponse(loginResponse, "sessionCookie");
            var requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.UsersController.Get(UserStatus.Active), loginResponse, "sessionCookie");

            HttpResponseMessage responsePage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            refreshToken.Should().NotBeNullOrEmpty();
            sessionCookie.Should().NotBeNullOrEmpty();
            loginResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            responsePage.StatusCode.Should().Be(HttpStatusCode.OK);
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
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "UsernameOrEmail", s_emailAddressNotConfirmedMessage } });
        }

        [Test]
        public async Task Should_Register_With_Existing_Person()
        {
            var registerDto = new UserRegisterDto
            {
                Email = "person@without.user",
                UserName = "personwithoutuser",
                Password = UserSeedData.ValidPassword,
                GivenName = "Person",
                Surname = "Without",
                ClientUri = "http://localhost:4200",
                GenderId = SelectValueMappingSeedData.PersonGenderMappings[0].Id,
                DateOfBirth = new DateTime(1950, 8, 9)
            };
            registerDto.StakeholderGroupIds.Add(SectionSeedData.Volunteers.Id);

            HttpClient client = _unAuthenticatedServer
                .CreateClient();

            // Act
            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
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
                GenderId = SelectValueMappingSeedData.PersonGenderMappings[0].Id,
                AboutMe = null
            };
            registerDto.StakeholderGroupIds.Add(SectionSeedData.Volunteers.Id);
            _fakeSmtpServer.ClearReceivedEmail();

            HttpClient client = _unAuthenticatedServer
                .CreateClient();

            // Act
            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.AuthController.Register(), BuildStringContent(registerDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);

            // Arrange
            var queryParameters = _fakeSmtpServer.ReceivedEmail[0].MessageParts[0].BodyData.Split("?token=")[1].Split("&email=");
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
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "Email", s_emailAlreadyExistsMessage } });
        }

        [Test]
        public async Task Should_Not_Register_Multiple_Persons_With_Same_Email()
        {
            // Arrange
            var registerDto = new UserRegisterDto
            {
                Surname = "Nachname",
                GivenName = "Vorname",
                Email = "person@withsame.email",
                UserName = "personwithsameemail",
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
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "Email", s_multiplePersonsFoundMessage } });
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
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "UserName", s_usernameAlreadyExistsMessage } });
        }

        [Test]
        public async Task Should_Change_Password()
        {
            // Arrange
            User user = FakeUsers.Admin;
            var loginDto = new LoginDto
            {
                UsernameOrEmail = user.UserName,
                Password = UserSeedData.ValidPassword
            };
            var passwordDto = new ChangePasswordDto
            {
                CurrentPassword = UserSeedData.ValidPassword,
                NewPassword = "NewPa$$w0rd"
            };

            //Act
            HttpResponseMessage loginResponse = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.AuthController.Password(), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(passwordDto);

            HttpResponseMessage responsePage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responsePage.StatusCode.Should().Be(HttpStatusCode.NoContent);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            SmtpMessage sentEmail = _fakeSmtpServer.ReceivedEmail[0];
            sentEmail.ToAddresses[0].Address.Should().BeEquivalentTo(user.Email);
        }

        [Test]
        public async Task Should_Not_Change_Wrong_Password()
        {
            // Arrange
            User user = FakeUsers.Admin;
            var loginDto = new LoginDto
            {
                UsernameOrEmail = user.UserName,
                Password = UserSeedData.ValidPassword
            };
            var passwordDto = new ChangePasswordDto
            {
                CurrentPassword = "WrongPassword",
                NewPassword = "NewPa$$w0rd"
            };

            // Act
            HttpResponseMessage loginResponse = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));


            var requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.AuthController.Password(), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(passwordDto);

            HttpResponseMessage responsePage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responsePage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responsePage);
            errorMessage.Title.Should().Be("One or more validation errors occurred.");
            errorMessage.Status.Should().Be(422);
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "CurrentPassword", s_incorrectPasswordMessage } });
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
                yield return new TestCaseData(FakeUsers.Admin, FakeUsers.Admin, new[] { RoleNames.Performer }, HttpStatusCode.UnprocessableEntity, false);
                yield return new TestCaseData(FakeUsers.Admin, FakeUsers.Admin, new[] { RoleNames.Admin, RoleNames.Performer }, HttpStatusCode.NoContent, true);
            }
        }

        private static readonly string[] s_incorrectPasswordMessage = ["Incorrect password supplied"];
        private static readonly string[] s_emailAlreadyConfirmedMessage = ["The email address is already confirmed"];
        private static readonly string[] s_emailAlreadyExistsMessage = ["Email aleady exists"];
        private static readonly string[] s_usernameAlreadyExistsMessage = ["Username aleady exists"];
        private static readonly string[] s_multiplePersonsFoundMessage = ["Multiple persons found with this email address. Registration aborted. Please contact your system admin."];
        private static readonly string[] s_emailAddressNotConfirmedMessage = ["Your email address is not confirmed. Please confirm your email address first"];


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
            var loginDto = new LoginDto
            {
                UsernameOrEmail = currentUser.UserName,
                Password = UserSeedData.ValidPassword
            };

            // Act
            HttpResponseMessage loginResponse = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));

            var requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.AuthController.SetRole(), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(setRoleDto);

            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

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
            var queryParameters = _fakeSmtpServer.ReceivedEmail[0].MessageParts[0].BodyData.Split("?token=")[1].Split("&email=");
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
            errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "UsernameOrEmail", s_emailAlreadyConfirmedMessage } });
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
            var firstCookie = GetCookieValueFromResponse(loginResult, "sessionCookie");

            Thread.Sleep(1000); // To ensure a significant difference between the access tokens

            // Act
            HttpResponseMessage responseMessage = await client.SendAsync(
                CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.AuthController.RefreshToken(), loginResult, "refreshToken"));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var secondCookie = GetCookieValueFromResponse(responseMessage, "sessionCookie");

            secondCookie.Should().NotBeNullOrEmpty();
            secondCookie.Should().NotBeEquivalentTo(firstCookie);

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

            // Act
            HttpResponseMessage loginResult = await client
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));
            TokenDto tokenDto = await DeserializeResponseMessageAsync<TokenDto>(loginResult);
            HttpRequestMessage request =
                CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.AuthController.Logout(), loginResult, "refreshToken");
            if (loginResult.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                SetCookieHeaderValue cookie = SetCookieHeaderValue.ParseList(values.ToImmutableList()).Single(cookie => cookie.Name == "sessionCookie");
                request.Headers.Add("Cookie", new CookieHeaderValue(cookie.Name, cookie.Value).ToString());
            }
            HttpResponseMessage responseMessage = await client.SendAsync(request);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
            HttpResponseMessage refreshMessage = await client.SendAsync(
                CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.AuthController.RefreshToken(), loginResult, "refreshToken"));
            refreshMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
