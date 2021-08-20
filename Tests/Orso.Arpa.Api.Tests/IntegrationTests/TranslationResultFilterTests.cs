using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class TranslationResultFilterTests : IntegrationTestBase
    {
        [SetUp]
        public void SetUp()
        {

        }

        // ToDo https://orso.atlassian.net/browse/ARPA-463?atlOrigin=eyJpIjoiOWY5Zjc2ZjMyZWFhNGNkNDgwZTI5M2FkZTc5ODEwYTciLCJwIjoiamlyYS1zbGFjay1pbnQifQ

        //[Test, Order(1)]
        //public async Task Should_Translate_Single_Section()
        //{
        //    var requestMessage = new HttpRequestMessage(HttpMethod.Get,ApiEndpoints.SectionsController.Get());
        //    requestMessage.Headers.Add("Accept-Language", "de-DE");

        //    HttpResponseMessage responseMessage = await _authenticatedServer
        //        .CreateClient()
        //        .AuthenticateWith(_performer)
        //        .SendAsync(requestMessage);

        //    IList<SectionDto> result = await DeserializeResponseMessageAsync<IList<SectionDto>>(responseMessage);

        //    result.AsQueryable().Where(s => s.Id == SectionDtoData.Performers.Id).Select(s => s.Name)
        //        .All(s => s.Equals("Künstler")).Should().BeTrue();

        //    result.AsQueryable().Where(s => s.Id == SectionDtoData.Orchestra.Id).Select(s => s.Name)
        //        .All(s => s.Equals("Orchester")).Should().BeTrue();
        //}

        //[Test, Order(2)]
        //public async Task Should_Translate_Roles()
        //{
        //    var requestMessage = new HttpRequestMessage(HttpMethod.Get,ApiEndpoints.RolesController.Get());
        //    requestMessage.Headers.Add("Accept-Language", "de-DE");

        //    HttpResponseMessage responseMessage = await _authenticatedServer
        //        .CreateClient()
        //        .AuthenticateWith(_staff)
        //        .SendAsync(requestMessage);

        //    IList<RoleDto> result = await DeserializeResponseMessageAsync<IList<RoleDto>>(responseMessage);

        //    result.AsQueryable().Where(s => s.Id == RoleDtoData.Performer.Id)
        //        .Select(s => s.RoleName).First().Should().Be("Künstler");

        //    result.AsQueryable().Where(s => s.Id == RoleDtoData.Staff.Id).Select(s => s.RoleName)
        //        .First().Should().Be("Mitarbeiter");

        //    result.AsQueryable().Where(s => s.Id == RoleDtoData.Admin.Id).Select(s => s.RoleName)
        //        .First().Should().Be("Administrator");
        //}
    }
}
