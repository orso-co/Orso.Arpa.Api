using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
        protected User _orsianer;
        protected User _orsonaut;
        protected User _orsoadmin;

        [OneTimeTearDown]
        public virtual void Cleanup()
        {
            _unAuthenticatedServer.Dispose();
            _authenticatedServer.Dispose();
        }

        [OneTimeSetUp]
        public virtual void Initialize()
        {
            _unAuthenticatedServer = CreateServer(false);
            _authenticatedServer = CreateServer(true);
            _orsianer = FakeUsers.Orsianer;
            _orsonaut = FakeUsers.Orsonaut;
            _orsoadmin = FakeUsers.Orsoadmin;
        }

        protected async Task<T> DeserializeResponseMessageAsync<T>(HttpResponseMessage responseMessage)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        protected StringContent BuildStringContent(object unserializedObject)
        {
            return new StringContent(
                JsonConvert.SerializeObject(unserializedObject),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);
        }

        protected async Task<TestServer> CreateServer(bool authenticated)
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
    }
}
