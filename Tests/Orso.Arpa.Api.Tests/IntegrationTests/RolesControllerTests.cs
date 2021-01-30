using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.RoleApplication;
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
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.RolesController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<RoleDto> result = await DeserializeResponseMessageAsync<IEnumerable<RoleDto>>(responseMessage);

            result.Should().BeEquivalentTo(expectedDtos);
        }
    }
}
