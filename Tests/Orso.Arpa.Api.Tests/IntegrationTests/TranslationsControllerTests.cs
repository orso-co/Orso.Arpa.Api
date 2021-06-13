using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.TranslationApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class TranslationsControllerTests : IntegrationTestBase
    {

        private LocalizationToTranslationConverter _localizationToTranslationConverter;

        [SetUp]
        public void SetUp()
        {
            _localizationToTranslationConverter = new LocalizationToTranslationConverter();
        }


        [Test, Order(1)]
        public async Task Should_Return_Translations()
        {
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .SendAsync(new HttpRequestMessage(HttpMethod.Get,ApiEndpoints.TranslationController.Get("de-DE")));

            TranslationDto expectedDto =
                _localizationToTranslationConverter.Convert(TranslationSeedData.Translations,
                    null, null);

            TranslationDto result = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(2)]
        public async Task Should_Change_Translations()
        {
            IList<Localization> localizations = TranslationSeedData.Translations;
            localizations.Add(new(new Guid("19E0484C-9BBC-40F5-8A42-52563205CF54"), "Trumpet",
                "Trompete", "de-DE", "SectionDto"));
            TranslationDto expectedDto =
                _localizationToTranslationConverter.Convert(localizations, null, null);

            HttpResponseMessage postResponseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PutAsync(ApiEndpoints.TranslationController.Put("de-DE"), BuildStringContent(expectedDto));

            postResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Get and check values.
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .SendAsync(new HttpRequestMessage(HttpMethod.Get,ApiEndpoints.TranslationController.Get("de-DE")));

            TranslationDto result = await DeserializeResponseMessageAsync<TranslationDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }
    }
}
