using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.ContactDetailApplication;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class PersonContactDetailsControllerTests : IntegrationTestBase
    {
        [Test, Order(100)]
        public async Task Should_Add_New_Contact_Detail_To_Existing_Person()
        {
            Domain.Entities.Person person = PersonTestSeedData.Performer;

            var dto = new ContactDetailCreateBodyDto
            {
                CommentTeam = "Teamkommentar",
                Key = ContactDetailKey.PhoneNumber,
                Preference = 2,
                TypeId = SelectValueMappingSeedData.ContactDetailTypeMappings[0].Id,
                Value = "+152-1234567"
            };

            var expectedDto = new ContactDetailDto
            {
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Staff Member",
                CommentTeam = dto.CommentTeam,
                Key = dto.Key,
                Preference = dto.Preference,
                TypeId = dto.TypeId,
                Value = dto.Value
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.PersonContactDetailsController
                    .Post(person.Id), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ContactDetailDto result = await DeserializeResponseMessageAsync<ContactDetailDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            result.Id.Should().NotBeEmpty();
        }

        [Test, Order(1000)]
        public async Task Should_Modify_Contact_Detail()
        {
            Domain.Entities.Person person = PersonTestSeedData.PersonWithoutUser;

            var dto = new ContactDetailModifyBodyDto
            {
                CommentTeam = "Teamkommentar",
                Key = ContactDetailKey.PhoneNumber,
                Preference = 2,
                TypeId = SelectValueMappingSeedData.ContactDetailTypeMappings[0].Id,
                Value = "+152-1234567"
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.PersonContactDetailsController
                    .Put(person.Id, person.ContactDetails.First().Id), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10000)]
        public async Task Should_Delete_Contact_Detail()
        {
            Domain.Entities.Person person = PersonTestSeedData.PersonWithoutUser;

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.PersonContactDetailsController
                    .Delete(person.Id, person.ContactDetails.First().Id));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
