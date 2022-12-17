using System.Collections.Generic;
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
using netDumbster.smtp;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.FakeData;
using Yoh.Text.Json.NamingPolicies;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    [SetUICulture("en-GB")]
    public abstract class IntegrationTestBase
    {
        protected TestServer _unAuthenticatedServer;
        protected TestServer _authenticatedServer;
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
            _authenticatedServer.Dispose();
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
            _unAuthenticatedServer = await CreateServer(false);
            _authenticatedServer = await CreateServer(true);
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

        protected static async Task<TestServer> CreateServer(bool authenticated)
        {
            IHostBuilder webHostBuilder = new HostBuilder();

            _ = webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());

            _ = authenticated
                ? webHostBuilder.ConfigureWebHost(webBuilder =>
                {
                    _ = webBuilder
                        .UseTestServer()
                        .UseEnvironment("Test")
                        .ConfigureAppConfiguration((_, config) => config.AddJsonFile("appsettings.Test.json"))
                        .UseStartup<AuthenticatedTestStartup>();
                })
                : webHostBuilder.ConfigureWebHost(webBuilder =>
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

        protected void EvaluateSimpleEmail(string expectedReceiverAddress, string expectedSubject)
        {
            _ = _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            SmtpMessage receivedEmail = _fakeSmtpServer.ReceivedEmail[0];
            _ = receivedEmail.Subject.Should().Be(expectedSubject);
            _ = receivedEmail.ToAddresses.Length.Should().Be(1);
            _ = receivedEmail.ToAddresses[0].Address.Should().Be(expectedReceiverAddress);
        }
    }
}
