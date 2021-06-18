using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.TranslationApplication;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class TranslationsControllerTests : IntegrationTestBase
    {
        [SetUp]
        public void SetUp()
        {

        }


        [Test, Order(1)]
        public async Task Should_Return_Translations()
        {
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .SendAsync(new HttpRequestMessage(HttpMethod.Get,ApiEndpoints.TranslationController.Get("de-DE")));

            TranslationDto result = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);

            Dictionary<string, string> roles = result.First(f => f.Key.Equals("RoleDto")).Value;

            roles.TryGetValue("Performer", out string kuenstler);
            kuenstler.Should().BeEquivalentTo("Künstler");

            roles.TryGetValue("Staff", out string mitarbeiter);
            mitarbeiter.Should().BeEquivalentTo("Mitarbeiter");

            roles.TryGetValue("Admin", out string administrator);
            administrator.Should().BeEquivalentTo("Administrator");
        }

        [Test, Order(2)]
        public async Task Should_Change_Translations()
        {
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .SendAsync(new HttpRequestMessage(HttpMethod.Get,ApiEndpoints.TranslationController.Get("de-DE")));

            TranslationDto expected = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);
            Dictionary<string, string> roles = expected.First(f => f.Key.Equals("RoleDto")).Value;
            roles.Add("TheOneAndOnly", "DerEinzigWahre");

            HttpResponseMessage postResponseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.TranslationController.Put("de-DE"), BuildStringContent(expected));

            postResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Get and check values.
            responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .SendAsync(new HttpRequestMessage(HttpMethod.Get,ApiEndpoints.TranslationController.Get("de-DE")));

            TranslationDto result = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
