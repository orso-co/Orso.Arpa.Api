using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Application.MyProjectApplication;

namespace Orso.Arpa.Api.Tests.IntegrationTests;

public class MyProjectsControllerTests : IntegrationTestBase
{
    [Test, Order(1)]
    public async Task Should_Get_My_Projects()
    {
        // Act
        HttpResponseMessage responseMessage = await _authenticatedServer
            .CreateClient()
            .AuthenticateWith(_performer)
            .GetAsync(ApiEndpoints.MyProjectsController.Get());

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        IList<MyProjectDto> result = await DeserializeResponseMessageAsync<IList<MyProjectDto>>(responseMessage);
       //  result.Should().BeEquivalentTo(expectedDtos, opt => opt.WithStrictOrdering());
    }
}
