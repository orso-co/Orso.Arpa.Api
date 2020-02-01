using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Logic.Projects;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class ProjectsControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_All()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .GetAsync(ApiEndpoints.ProjectsController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectDto>>(responseMessage);
            result.Should().BeEquivalentTo(ProjectDtoData.Projects, opt => opt.Excluding(dto => dto.CreatedAt));
        }
    }
}
