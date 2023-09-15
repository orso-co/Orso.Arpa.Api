using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.ContactDetailApplication.Model;
using Orso.Arpa.Application.MyContactDetailApplication.Model;
using Orso.Arpa.Domain.PersonDomain.Enums;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class MyContactDetailsControllerTests : IntegrationTestBase
    {
        [Test, Order(100)]
        public async Task Should_Add_New_Contact_Detail_To_Existing_Person()
        {
            var dto = new MyContactDetailCreateDto
            {
                CommentInner = "Kommentar",
                Key = ContactDetailKey.PhoneNumber,
                Preference = 2,
                TypeId = SelectValueMappingSeedData.ContactDetailTypeMappings[0].Id,
                Value = "+152-1234567"
            };

            var expectedDto = new ContactDetailDto
            {
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Per Former",
                CommentInner = dto.CommentInner,
                Key = dto.Key,
                Preference = dto.Preference,
                TypeId = dto.TypeId,
                Value = dto.Value
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .PostAsync(ApiEndpoints.MyContactDetailsController
                    .Post(), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ContactDetailDto result = await DeserializeResponseMessageAsync<ContactDetailDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            result.Id.Should().NotBeEmpty();
        }

        [Test, Order(1000)]
        public async Task Should_Modify_Contact_Detail()
        {
            Person person = PersonTestSeedData.UserWithoutRole;

            var dto = new MyContactDetailModifyBodyDto
            {
                CommentInner = "Kommentar Inner",
                Key = ContactDetailKey.PhoneNumber,
                Preference = 2,
                TypeId = SelectValueMappingSeedData.ContactDetailTypeMappings[0].Id,
                Value = "+152-1234567"
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(UserTestSeedData.UserWithoutRole)
                .PutAsync(ApiEndpoints.MyContactDetailsController
                    .Put(person.ContactDetails.First().Id), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10000)]
        public async Task Should_Delete_Contact_Detail()
        {
            Person person = PersonTestSeedData.UserWithoutRole;

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(UserTestSeedData.UserWithoutRole)
                .DeleteAsync(ApiEndpoints.MyContactDetailsController
                    .Delete(person.ContactDetails.First().Id));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
