using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.BankAccountApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class PersonBankAccountsControllerTests : IntegrationTestBase
    {
        [Test, Order(100)]
        public async Task Should_Add_New_Bank_Account_To_Existing_Person()
        {
            Domain.Entities.Person person = PersonTestSeedData.Performer;

            var dto = new BankAccountCreateBodyDto
            {
                Iban = "DE95680900000037156400",
                Bic = "GENODE61FR1",
                CommentInner = "Dieses Konto läuft auf meine Mudda",
                AccountOwner = "Muddi Roese"
            };

            var expectedDto = new BankAccountDto
            {
                Iban = "DE95680900000037156400",
                Bic = "GENODE61FR1",
                CommentInner = "Dieses Konto läuft auf meine Mudda",
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Staff Member",
                AccountOwner = "Muddi Roese"
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.PersonBankAccountsController
                    .Post(person.Id), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            BankAccountDto result = await DeserializeResponseMessageAsync<BankAccountDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            result.Id.Should().NotBeEmpty();
        }

        [Test, Order(1000)]
        public async Task Should_Modify_Bank_Account()
        {
            Domain.Entities.Person person = PersonTestSeedData.UnconfirmedUser;

            var dto = new BankAccountModifyBodyDto
            {
                Iban = "DE51500105178514758896",
                Bic = "GENODE61FR2",
                CommentInner = "Dieses Konto läuft auf meine Vadda",
                AccountOwner = "Daddy Roese",
                StatusId = SelectValueMappingSeedData.BankAccountStatusMappings[0].Id
            };

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.PersonBankAccountsController
                    .Put(person.Id, person.BankAccounts.First().Id), BuildStringContent(dto));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10000)]
        public async Task Should_Delete_Bank_Account()
        {
            Domain.Entities.Person person = PersonTestSeedData.UnconfirmedUser;

            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.PersonBankAccountsController
                    .Delete(person.Id, person.BankAccounts.First().Id));

            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
