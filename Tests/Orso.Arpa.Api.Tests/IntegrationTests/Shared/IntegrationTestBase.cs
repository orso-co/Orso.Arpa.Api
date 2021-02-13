using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using netDumbster.smtp;
using Newtonsoft.Json;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    public abstract class IntegrationTestBase
    {
        protected TestServer _unAuthenticatedServer;
        protected TestServer _authenticatedServer;
        protected User _performer;
        protected User _staff;
        protected User _admin;
        protected SimpleSmtpServer _fakeSmtpServer;

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
        }

        [SetUp]
        public void SetupMailServer()
        {
            _fakeSmtpServer = Configuration.Configure()
                 .WithPort(25)
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
        }

        protected static async Task<T> DeserializeResponseMessageAsync<T>(HttpResponseMessage responseMessage)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        protected static StringContent BuildStringContent(object unserializedObject)
        {
            return new StringContent(
                JsonConvert.SerializeObject(unserializedObject),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);
        }

        protected static async Task<TestServer> CreateServer(bool authenticated)
        {
            IHostBuilder webHostBuilder = new HostBuilder();

            webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());

            if (authenticated)
            {
                webHostBuilder.ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .UseEnvironment("Test")
                        .ConfigureAppConfiguration((_, config) => config.AddJsonFile("appsettings.Test.json"))
                        .UseStartup<AuthenticatedTestStartup>();
                });
            }
            else
            {
                webHostBuilder.ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .UseEnvironment("Test")
                        .ConfigureAppConfiguration((_, config) => config.AddJsonFile("appsettings.Test.json"))
                        .UseStartup<TestStartup>();
                });
            }

            IHost host = await webHostBuilder.StartAsync();
            return host.GetTestServer();
        }

        protected static string GetCookieValueFromResponse(HttpResponseMessage response, string cookieName)
        {
            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            return cookies.FirstOrDefault(cookie => cookie.StartsWith(cookieName));
        }
    }
}
