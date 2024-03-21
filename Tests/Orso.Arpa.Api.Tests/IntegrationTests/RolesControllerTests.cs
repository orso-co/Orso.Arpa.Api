using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.RoleApplication.Model;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class RolesControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_All_Roles()
        {
            // Arrange
            IList<RoleDto> expectedDtos = RoleDtoData.Roles;

            // Act
            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Get, ApiEndpoints.RolesController.Get(), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<RoleDto> result = await DeserializeResponseMessageAsync<IEnumerable<RoleDto>>(responseMessage);

            result.Should().BeEquivalentTo(expectedDtos);
        }
    }
}
