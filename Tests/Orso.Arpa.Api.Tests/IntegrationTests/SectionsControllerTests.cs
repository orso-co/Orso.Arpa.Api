using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class SectionsControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_All()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.SectionsController.Get());

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<SectionDto> result = await DeserializeResponseMessageAsync<IEnumerable<SectionDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(SectionDtoData.Sections);
        }

        [Test, Order(2)]
        public async Task Should_Get_Tree()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.SectionsController.GetTree(2));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            SectionTreeDto result = await DeserializeResponseMessageAsync<SectionTreeDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(SectionTreeDtoData.Level2SectionTreeDto);
        }

        [Test, Order(3)]
        public async Task Should_Get_Instruments_With_Children()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.SectionsController.GetInstrumentsWithChildren());

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<SectionDto> result = await DeserializeResponseMessageAsync<IEnumerable<SectionDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(SectionDtoData.InstrumentsWithChildren);
        }

        private static IEnumerable<TestCaseData> s_doublingInstrumentsData
        {
            get
            {
                yield return new TestCaseData(SectionSeedData.Flute.Id, new List<SectionDto>
                {
                    SectionDtoData.PiccoloFlute,
                    SectionDtoData.AltoFlute,
                    SectionDtoData.BassFlute,
                    SectionDtoData.TenorFlute
                });
                yield return new TestCaseData(SectionSeedData.PiccoloFlute.Id, new List<SectionDto>
                {
                    SectionDtoData.Flute,
                    SectionDtoData.AltoFlute,
                    SectionDtoData.BassFlute,
                    SectionDtoData.TenorFlute
                });
                yield return new TestCaseData(SectionSeedData.Band.Id, new List<SectionDto>());
            }
        }

        [Test, Order(4)]
        [TestCaseSource(nameof(s_doublingInstrumentsData))]
        public async Task Should_Get_DoublingInstruments(Guid sectionId, IEnumerable<SectionDto> expectedResult)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.SectionsController.GetDoublingInstruments(sectionId));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<SectionDto> result = await DeserializeResponseMessageAsync<IEnumerable<SectionDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Should_Get_Positions()
        {
            var expectedResult = new List<SelectValueDto>
            {
                  new SelectValueDto { Id = Guid.Parse("640eaff9-0234-46cb-8dfe-2ba97399e6d3"), Name = "Solo", Description = "" },
                  new SelectValueDto { Id = Guid.Parse("7b01cc1c-15c7-4d66-8971-d2bf5507a676"), Name = "Section lead", Description = "" },
                  new SelectValueDto { Id = Guid.Parse("de6a82d3-4374-491d-8125-dca3d55dcdf1"), Name = "High", Description = "" },
                  new SelectValueDto { Id = Guid.Parse("f85ecc0c-f793-49ee-a7e1-780edde12ec5"), Name = "Low", Description = "" },
                  new SelectValueDto { Id = Guid.Parse("6993ab28-3a79-4941-8a14-f07bdae5a3ba"), Name = "Coach", Description = "" },
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.SectionsController.GetPositions(SectionSeedData.Alto.Id));

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<SelectValueDto> result = await DeserializeResponseMessageAsync<IEnumerable<SelectValueDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedResult);
        }

        [Test, Order(5)]
        public async Task Should_Return_Too_Many_Requests()
        {
            HttpClient client = _unAuthenticatedServer.CreateClient();
            HttpResponseMessage responseMessage = null;

            // Act
            for (int i = 0; i < 2; i++)
            {
                responseMessage = await client
                    .GetAsync(ApiEndpoints.SectionsController.GetTree(2));
            }

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            _ = responseString.Should().Be("API calls quota exceeded! maximum admitted 1 per 1s.");
        }
    }
}
