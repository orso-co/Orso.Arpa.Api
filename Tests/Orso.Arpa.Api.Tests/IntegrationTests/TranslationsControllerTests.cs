using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.SectionApplication;
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

            _ = sections.TryGetValue("Performers", out string kuenstler);
            _ = kuenstler.Should().BeEquivalentTo("Mitwirkende");

            _ = sections.TryGetValue("Orchestra", out string orchester);
            _ = orchester.Should().BeEquivalentTo("Orchester");

            _ = sections.TryGetValue("Bass", out string bass);
            _ = bass.Should().BeEquivalentTo("Bass (Chor)");
        }

        [Test, Order(2)]
        public async Task Should_Change_Translations()
        {
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .SendAsync(new HttpRequestMessage(HttpMethod.Get, ApiEndpoints.TranslationController.Get("de")));

            TranslationDto expected = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);
            expected.First(f => f.Key.Equals(nameof(SectionDto))).Value.Add("TheOneAndOnly", "DerEinzigWahre");

            HttpResponseMessage postResponseMessage = await client
                .PutAsync(ApiEndpoints.TranslationController.Put("de"), BuildStringContent(expected));

            _ = postResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Get and check values.
            responseMessage = await client
                .SendAsync(new HttpRequestMessage(HttpMethod.Get, ApiEndpoints.TranslationController.Get("de")));

            TranslationDto result = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);

            _ = result.Should().BeEquivalentTo(expected);
        }
    }
}
