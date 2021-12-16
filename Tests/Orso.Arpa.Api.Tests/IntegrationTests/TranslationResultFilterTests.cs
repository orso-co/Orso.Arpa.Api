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
        [TestCase("de-DE", "Mitwirkende", "Orchester")]
        [TestCase("de", "Mitwirkende", "Orchester")]
        [TestCase("en", "Performers", "Orchestra")]
        [TestCase("en,de-DE;q=0.7,de;q=0.3", "Performers", "Orchestra")]
        public async Task Should_Translate_Single_Section(string header, string firstCheck, string secondCheck)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, ApiEndpoints.SectionsController.Get());
            requestMessage.Headers.Add("Accept-Language", header);

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .SendAsync(requestMessage);

            IList<SectionDto> result = await DeserializeResponseMessageAsync<IList<SectionDto>>(responseMessage);

            result
                .First(s => s.Id == SectionDtoData.Performers.Id)
                .Name.Should().Be(firstCheck);

            result
                .First(s => s.Id == SectionDtoData.Orchestra.Id)
                .Name.Should().Be(secondCheck);
        }

        [Test, Order(2)]
        public async Task Should_Translate_Roles()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, ApiEndpoints.RolesController.Get());
            requestMessage.Headers.Add("Accept-Language", "de-DE");

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .SendAsync(requestMessage);

            IList<RoleDto> result = await DeserializeResponseMessageAsync<IList<RoleDto>>(responseMessage);

            result.AsQueryable().Where(s => s.Id == RoleDtoData.Performer.Id)
                .Select(s => s.RoleName).First().Should().Be("Mitwirkender");

            result.AsQueryable().Where(s => s.Id == RoleDtoData.Staff.Id).Select(s => s.RoleName)
                .First().Should().Be("Mitarbeiter");

            result.AsQueryable().Where(s => s.Id == RoleDtoData.Admin.Id).Select(s => s.RoleName)
                .First().Should().Be("Administrator");
        }
    }
}
