using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.RoleApplication;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class TranslationResultFilterTests : IntegrationTestBase
    {
        [SetUp]
        public void SetUp()
        {

        }


        [Test, Order(1)]
        public async Task Should_Translate_Single_Section()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,ApiEndpoints.SectionsController.Get());
            requestMessage.Headers.Add("Accept-Language", "de-DE");

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .SendAsync(requestMessage);

            IEnumerable<SectionDto> result = await DeserializeResponseMessageAsync<IEnumerable<SectionDto>>(responseMessage);

            result.AsQueryable().Where(s => s.Id == SectionDtoData.Flute.Id).Select(s => s.Name)
                .All(s => s.Equals("Flöte")).Should().BeTrue();

            result.AsQueryable().Where(s => s.Id == SectionDtoData.Violins.Id).Select(s => s.Name)
                .All(s => s.Equals("Violinen")).Should().BeTrue();
        }

        [Test, Order(2)]
        public async Task Should_Translate_Roles()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,ApiEndpoints.RolesController.Get());
            requestMessage.Headers.Add("Accept-Language", "de-DE");

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .SendAsync(requestMessage);

            IEnumerable<RoleDto> result = await DeserializeResponseMessageAsync<IEnumerable<RoleDto>>(responseMessage);

            result.AsQueryable().Where(s => s.Id == RoleDtoData.Performer.Id)
                .Select(s => s.RoleName).First().Should().Be("Künstler");

            result.AsQueryable().Where(s => s.Id == RoleDtoData.Staff.Id).Select(s => s.RoleName)
                .First().Should().Be("Mitarbeiter");

            result.AsQueryable().Where(s => s.Id == RoleDtoData.Admin.Id).Select(s => s.RoleName)
                .First().Should().Be("Administrator");
        }
    }
}
