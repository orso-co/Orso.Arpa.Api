using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
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

        [OneTimeTearDown]
        public virtual void Cleanup()
        {
            _unAuthenticatedServer.Dispose();
        }

        [OneTimeSetUp]
        public virtual void Initialize()
        {
            _unAuthenticatedServer = CreateServer();
        }

        protected StringContent BuildStringContent(object unserializedObject)
        {
            return new StringContent(
                JsonConvert.SerializeObject(unserializedObject),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);
        }

        protected TestServer CreateServer()
        {
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder();

            webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());

            webHostBuilder.UseStartup<TestStartup>();

            webHostBuilder.UseEnvironment("Test");

            webHostBuilder.ConfigureAppConfiguration((_, config) => config.AddJsonFile("appsettings.Test.json"));

            return new TestServer(webHostBuilder);
        }
    }
}
