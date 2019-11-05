using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    public abstract class IntegrationTestBase
    {
        protected TestServer _unAuthenticatedServer;
        protected TestServer _authenticatedServer;

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

        protected TestServer CreateServer(bool authenticated)
        {
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder();

            webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());

            if (authenticated)
            {
                webHostBuilder.UseStartup<AuthenticatedTestStartup>();
            }
            else
            {
                webHostBuilder.UseStartup<TestStartup>();
            }

            webHostBuilder.UseEnvironment("Test");

            webHostBuilder.ConfigureAppConfiguration((_, config) => config.AddJsonFile("appsettings.Test.json"));

            return new TestServer(webHostBuilder);
        }
    }
}
