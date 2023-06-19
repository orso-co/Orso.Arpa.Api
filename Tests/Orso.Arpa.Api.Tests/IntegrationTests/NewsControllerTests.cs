using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.NewsApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class NewsControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_All()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.NewsController.Get(1,1,true));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<NewsDto> result = await DeserializeResponseMessageAsync<IEnumerable<NewsDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(NewsDtoData.News, opt => opt.WithStrictOrdering());
        }

        [Test, Order(2)]
        public async Task Should_Get_ById()
        {
            // Arrange
            NewsDto expectedDto = NewsDtoData.Performer;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.NewsController.Get(expectedDto.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
           NewsDto result = await DeserializeResponseMessageAsync<NewsDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
        }
        [Test, Order(100)]
        public async Task Should_Modify()
        {
            // Arrange
            News newsToModify = NewsSeedData.ErsteNews;
            var modifyDto = new NewsModifyBodyDto
            {
                NewsText = "ErsteNewsModifiziert",
                Url = "http://orsopolis.com",
                Show = false,
            };

            var expectedDto = new NewsDto
            {
                Id = newsToModify.Id,
                NewsText = "ErsteNewsModifiziert",
                Url = "http://orsopolis.com",
                Show = false,
            };

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.NewsController.Put(newsToModify.Id), BuildStringContent(modifyDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.NewsController.Get(newsToModify.Id));

            _ = getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            NewsDto result = await DeserializeResponseMessageAsync<NewsDto>(getMessage);

            _ = result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(10000)]
        public async Task Should_Delete()
        {
            // Arrange

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.NewsController.Delete(NewsSeedData.ZweiteNews.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
