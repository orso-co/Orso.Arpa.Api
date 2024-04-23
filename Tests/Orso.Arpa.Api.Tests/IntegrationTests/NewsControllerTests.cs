using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.NewsApplication.Model;
using Orso.Arpa.Domain.NewsDomain.Model;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests;

[TestFixture]
public class NewsControllerTests : IntegrationTestBase
{
    [Test]
    [Order(1)]
    public async Task Should_Get_All()

    {
        // Arrange
        var expectedDtos = new List<NewsDto> { NewsDtoData.SecondNews };

        // Act
        HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
        HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.NewsController.Get(1, 1, true), loginResponse, "sessionCookie");
        HttpResponseMessage responseMessage = await _unAuthenticatedServer
            .CreateClient().SendAsync(requestMessage);

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        IEnumerable<NewsDto> result =
            await DeserializeResponseMessageAsync<IEnumerable<NewsDto>>(responseMessage);
        _ = result.Should().BeEquivalentTo(expectedDtos, opt => opt.WithStrictOrdering());
    }

    [Test]
    [Order(20)]
    public async Task Should_Create([Values(null, "https://backstage.orso.co")] string url)
    {
        // Arrange
        var createDto = new NewsCreateDto
        {
            Title = "New News Title",
            Content = "New News Text",
            Url = url,
            Show = true
        };

        var expectedDto = new NewsDto
        {
            Title = createDto.Title,
            Content = createDto.Content,
            Url = url,
            Show = true,
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = _staff.DisplayName
        };

        // Act
        HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
        HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.NewsController.Post(), loginResponse, "sessionCookie");
        requestMessage.Content = BuildStringContent(createDto);
        HttpResponseMessage responseMessage = await _unAuthenticatedServer
            .CreateClient().SendAsync(requestMessage);

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
        NewsDto result = await DeserializeResponseMessageAsync<NewsDto>(responseMessage);

        _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id));
        _ = result.Id.Should().NotBeEmpty();
        _ = responseMessage.Headers.Location.AbsolutePath.Should()
            .Be($"/{ApiEndpoints.NewsController.Get(result.Id)}");
    }

    [Test]
    [Order(100)]
    public async Task Should_Modify([Values(null, "https://orsopolis.com")] string url)
    {
        // Arrange
        News newsToModify = NewsSeedData.FirstNews;
        var modifyDto = new NewsModifyBodyDto
        {
            Title = "Title der ersten Message modifiziert",
            Content = "ErsteNewsModifiziert",
            Url = url,
            Show = false
        };

        var expectedDto = new NewsDto
        {
            Id = newsToModify.Id,
            Title = "Title der ersten Message modifiziert",
            Content = "ErsteNewsModifiziert",
            Url = url,
            Show = false,
            CreatedBy = "anonymous",
            CreatedAt = FakeDateTime.UtcNow,
            ModifiedAt = FakeDateTime.UtcNow,
            ModifiedBy = "Staff Member"
        };
        HttpResponseMessage loginResponse = await LoginUserAsync(_staff);

        // Act
        HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.NewsController.Put(newsToModify.Id), loginResponse, "sessionCookie");
        requestMessage.Content = BuildStringContent(modifyDto);
        HttpResponseMessage responseMessage = await _unAuthenticatedServer
            .CreateClient().SendAsync(requestMessage);

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

        HttpRequestMessage requestMessage2 = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.NewsController.Get(newsToModify.Id), loginResponse, "sessionCookie");
        requestMessage.Content = BuildStringContent(modifyDto);
        HttpResponseMessage getMessage = await _unAuthenticatedServer
            .CreateClient().SendAsync(requestMessage2);

        _ = getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        NewsDto result = await DeserializeResponseMessageAsync<NewsDto>(getMessage);

        _ = result.Should().BeEquivalentTo(expectedDto);
    }

    [Test]
    [Order(10000)]
    public async Task Should_Delete()
    {
        // Arrange

        // Act
        HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
        HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.NewsController.Delete(NewsSeedData.SecondNews.Id), loginResponse, "sessionCookie");
        HttpResponseMessage responseMessage = await _unAuthenticatedServer
            .CreateClient().SendAsync(requestMessage);

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
