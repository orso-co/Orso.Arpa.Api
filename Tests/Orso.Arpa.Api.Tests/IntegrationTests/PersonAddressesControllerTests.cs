using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.AddressApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class PersonAddressesControllerTests : IntegrationTestBase
    {
        [Test, Order(100)]
        public async Task Should_Add_New_Address_To_Existing_Person()
        {
            var dto = new AddressCreateBodyDto
            {
                UrbanDistrict = "Westend Süd",
                Address1 = "Bockenheimer Warte 33",
                Address2 = "c/o Frau Rotkohl",
                City = "Frankfurt am Main",
                State = "Hessen",
                Country = "Deutschland",
                Zip = "60325",
                CommentInner = "Kommentar Adresse",
                TypeId = SelectValueMappingSeedData.AddressTypeMappings[0].Id
            };

            var expectedDto = new AddressDto
            {
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Per Former",
                UrbanDistrict = "Westend Süd",
                Address1 = "Bockenheimer Warte 33",
                Address2 = "c/o Frau Rotkohl",
                City = "Frankfurt am Main",
                CommentInner = "Kommentar Adresse",
                Country = "Deutschland",
                State = "Hessen",
                Type = SelectValueDtoData.PrivateAddress,
                Zip = "60325"
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.PersonAddressesController
                    .Post(_performer.PersonId), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            AddressDto result = await DeserializeResponseMessageAsync<AddressDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            result.Id.Should().NotBeEmpty();
        }

        [Test, Order(1000)]
        public async Task Should_Modify_Address()
        {
            Domain.Entities.Person person = PersonTestSeedData.UserWithoutRole;

            var dto = new AddressModifyBodyDto
            {
                UrbanDistrict = "Westend Süd",
                Address1 = "Bockenheimer Warte 33",
                Address2 = "c/o Frau Rotkohl",
                City = "Frankfurt am Main",
                State = "Hessen",
                Country = "Deutschland",
                Zip = "60325",
                CommentInner = "Kommentar Adresse",
                TypeId = SelectValueMappingSeedData.AddressTypeMappings[0].Id
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.PersonAddressesController
                    .Put(person.Id, person.Addresses.First().Id), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10000)]
        public async Task Should_Delete_Address()
        {
            Domain.Entities.Person person = PersonTestSeedData.UserWithoutRole;

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.PersonAddressesController
                    .Delete(person.Id, person.Addresses.First().Id));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task Should_Not_Add_Address_To_Different_Person()
        {
            var dto = new AddressCreateBodyDto();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.PersonAddressesController.Post(_staff.PersonId), BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
