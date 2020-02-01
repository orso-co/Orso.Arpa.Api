using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Logic.SelectValues;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class SelectValuesControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_All()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .GetAsync(ApiEndpoints.SelectValuesController.Get(nameof(Project), nameof(Project.Genre)));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<SelectValueDto> result = await DeserializeResponseMessageAsync<IEnumerable<SelectValueDto>>(responseMessage);
            result.Should().BeEquivalentTo(SelectValueDtoData.ProjectGenres, opt => opt.Excluding(dto => dto.CreatedAt));
        }
    }
}
