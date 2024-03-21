using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using netDumbster.smtp;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Yoh.Text.Json.NamingPolicies;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    [SetUICulture("en-GB")]
    public abstract class IntegrationTestBase
    {
        protected TestServer _unAuthenticatedServer;
        protected User _performer;
        protected User _staff;
        protected User _admin;
        protected SimpleSmtpServer _fakeSmtpServer;
        private JsonSerializerOptions _jsonSerializerOptions;

        [TearDown]
        public void DisposeMailServer()
        {
            if (_fakeSmtpServer != null)
            {
                _fakeSmtpServer.Stop();
                _fakeSmtpServer.Dispose();
            }
        }

        [OneTimeTearDown]
        public virtual void Cleanup()
        {
            _unAuthenticatedServer.Dispose();
            TestStartup.TestDatabase?.Drop();
            TestStartup.TestDatabase = null;
            TestStartup.IsSeeded = false;
        }

        [SetUp]
        public void SetupMailServer()
        {
            _fakeSmtpServer = Configuration.Configure()
                 .WithPort(2600)
                 .Build();
        }

        [OneTimeSetUp]
        public virtual async Task InitializeAsync()
        {
            _unAuthenticatedServer = await CreateServer();
            _performer = FakeUsers.Performer;
            _staff = FakeUsers.Staff;
            _admin = FakeUsers.Admin;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicies.SnakeCaseUpper));
        }

        protected async Task<T> DeserializeResponseMessageAsync<T>(HttpResponseMessage responseMessage)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseString))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(responseString, _jsonSerializerOptions);
        }

        protected StringContent BuildStringContent(object unserializedObject)
        {
            var serialized = JsonSerializer.Serialize(unserializedObject, _jsonSerializerOptions);
            return new StringContent(
                serialized,
                Encoding.UTF8,
                MediaTypeNames.Application.Json);
        }

        protected static async Task<TestServer> CreateServer()
        {
            IHostBuilder webHostBuilder = new HostBuilder();

            _ = webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());

            _ = webHostBuilder.ConfigureWebHost(webBuilder =>
                {
                    _ = webBuilder
                        .UseTestServer()
                        .UseEnvironment("Test")
                        .ConfigureAppConfiguration((_, config) => config.AddJsonFile("appsettings.Test.json"))
                        .UseStartup<TestStartup>();
                });

            IHost host = await webHostBuilder.StartAsync();
            return host.GetTestServer();
        }

        protected static string GetCookieValueFromResponse(HttpResponseMessage response, string cookieName)
        {
            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            return cookies.FirstOrDefault(cookie => cookie.StartsWith(cookieName));
        }

        protected void EvaluateSimpleEmail(string expectedSubject, params string[] expectedReceiverAddresses)
        {
            _ = _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            SmtpMessage receivedEmail = _fakeSmtpServer.ReceivedEmail[0];
            _ = receivedEmail.Subject.Should().Be(expectedSubject);
            _ = receivedEmail.ToAddresses.Length.Should().Be(expectedReceiverAddresses.Length);
            _ = receivedEmail.ToAddresses.Select(ta => ta.Address).Should().BeEquivalentTo(expectedReceiverAddresses);
        }

        protected async Task<HttpResponseMessage> LoginUserAsync(User user)
        {
            var loginDto = new LoginDto
            {
                UsernameOrEmail = user.UserName,
                Password = UserSeedData.ValidPassword
            };

            return await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.AuthController.Login(), BuildStringContent(loginDto));
            ;
        }

        protected static HttpRequestMessage CreateRequestWithCookie(HttpMethod httpMethod, string path, HttpResponseMessage response, string cookieName)
        {
            var request = new HttpRequestMessage(httpMethod, path);
            if (response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                SetCookieHeaderValue cookie = SetCookieHeaderValue.ParseList(values.ToImmutableList()).Single(cookie => cookie.Name == cookieName);
                request.Headers.Add("Cookie", new CookieHeaderValue(cookie.Name, cookie.Value).ToString());
            }

            return request;
        }
    }
}
