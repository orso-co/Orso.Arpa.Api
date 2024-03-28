using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Application.TranslationApplication.Model;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class TranslationsControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Return_Translations()
        {
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.TranslationController.Get("de"), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

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
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.TranslationController.Get("de"), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            TranslationDto expected = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);
            expected.First(f => f.Key.Equals(nameof(SectionDto))).Value.Add("TheOneAndOnly", "DerEinzigWahre");

            HttpRequestMessage requestMessage2 = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.TranslationController.Put("de"), loginResponse, "sessionCookie");
            requestMessage2.Content = BuildStringContent(expected);
            HttpResponseMessage postResponseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage2);

            _ = postResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Get and check values.
            HttpRequestMessage requestMessage3 = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.TranslationController.Get("de"), loginResponse, "sessionCookie");
            requestMessage2.Content = BuildStringContent(expected);
            HttpResponseMessage responseMessage2 = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage3);

            TranslationDto result = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage2);

            _ = result.Should().BeEquivalentTo(expected);
        }
    }
}
