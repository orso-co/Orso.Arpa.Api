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
        [Test, Order(1)]
        public async Task Should_Return_Translations()
        {
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .SendAsync(new HttpRequestMessage(HttpMethod.Get, ApiEndpoints.TranslationController.Get("de")));

            TranslationDto result = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);

            Dictionary<string, string> sections = result.First(f => f.Key.Equals("SectionDto")).Value;

            sections.TryGetValue("Performers", out string kuenstler);
            kuenstler.Should().BeEquivalentTo("Mitwirkende");

            sections.TryGetValue("Orchestra", out string orchester);
            orchester.Should().BeEquivalentTo("Orchester");

            sections.TryGetValue("Bass", out string Bass);
            Bass.Should().BeEquivalentTo("Bass");
        }

        [Test, Order(2)]
        public async Task Should_Change_Translations()
        {
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .SendAsync(new HttpRequestMessage(HttpMethod.Get, ApiEndpoints.TranslationController.Get("de")));

            TranslationDto expected = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);
            Dictionary<string, string> sections = expected.First(f => f.Key.Equals("SectionDto")).Value;
            sections.Add("TheOneAndOnly", "DerEinzigWahre");

            HttpResponseMessage postResponseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.TranslationController.Put("de"), BuildStringContent(expected));

            postResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Get and check values.
            responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .SendAsync(new HttpRequestMessage(HttpMethod.Get, ApiEndpoints.TranslationController.Get("de")));

            TranslationDto result = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
