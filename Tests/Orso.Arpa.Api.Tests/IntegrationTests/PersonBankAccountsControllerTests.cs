using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.BankAccountApplication.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class PersonBankAccountsControllerTests : IntegrationTestBase
    {
        [Test, Order(100)]
        public async Task Should_Add_New_Bank_Account_To_Existing_Person_As_Staff()
        {
            Person person = PersonTestSeedData.Performer;

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

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.PersonBankAccountsController
                    .Post(person.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(dto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            BankAccountDto result = await DeserializeResponseMessageAsync<BankAccountDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            _ = result.Id.Should().NotBeEmpty();
        }

        [Test, Order(101)]
        public async Task Should_Add_New_Bank_Account_To_Existing_Person_As_Performer()
        {
            Person person = PersonTestSeedData.Performer;

            var dto = new BankAccountCreateBodyDto
            {
                Iban = "DE95680900000037156401",
                Bic = "GENODE61FR1",
                CommentInner = "Dieses Konto läuft auf meine Mudda",
                AccountOwner = "Muddi Roese"
            };

            var expectedDto = new BankAccountDto
            {
                Iban = "DE95680900000037156401",
                Bic = "GENODE61FR1",
                CommentInner = "Dieses Konto läuft auf meine Mudda",
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Per Former",
                AccountOwner = "Muddi Roese"
            };

            HttpResponseMessage loginResponse = await LoginUserAsync(_performer);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Post, ApiEndpoints.PersonBankAccountsController
                    .Post(person.Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(dto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            BankAccountDto result = await DeserializeResponseMessageAsync<BankAccountDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            _ = result.Id.Should().NotBeEmpty();
        }

        [Test, Order(102)]
        public async Task Should_Not_Add_New_Bank_Account_To_Existing_Person_When_User_Is_Not_Authenticated()
        {
            Person person = PersonTestSeedData.Performer;

            var dto = new BankAccountCreateBodyDto
            {
                Iban = "DE95680900000037156400",
                Bic = "GENODE61FR1",
                CommentInner = "Dieses Konto läuft auf meine Mudda",
                AccountOwner = "Muddi Roese"
            };

            _ = new BankAccountDto
            {
                Iban = "DE95680900000037156400",
                Bic = "GENODE61FR1",
                CommentInner = "Dieses Konto läuft auf meine Mudda",
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Staff Member",
                AccountOwner = "Muddi Roese"
            };

            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient()
                .PostAsync(ApiEndpoints.PersonBankAccountsController
                    .Post(person.Id), BuildStringContent(dto));

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test, Order(1000)]
        public async Task Should_Modify_Bank_Account()
        {
            Person person = PersonTestSeedData.UnconfirmedUser;

            var dto = new BankAccountModifyBodyDto
            {
                Iban = "DE51500105178514758896",
                Bic = "GENODE61FR2",
                CommentInner = "Dieses Konto läuft auf meine Vadda",
                AccountOwner = "Daddy Roese",
                StatusId = SelectValueMappingSeedData.BankAccountStatusMappings[0].Id
            };

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Put, ApiEndpoints.PersonBankAccountsController
                    .Put(person.Id, person.BankAccounts.First().Id), loginResponse, "sessionCookie");
            requestMessage.Content = BuildStringContent(dto);
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(10000)]
        public async Task Should_Delete_Bank_Account()
        {
            Person person = PersonTestSeedData.UnconfirmedUser;

            HttpResponseMessage loginResponse = await LoginUserAsync(_staff);
            HttpRequestMessage requestMessage = CreateRequestWithCookie(HttpMethod.Delete, ApiEndpoints.PersonBankAccountsController
                    .Delete(person.Id, person.BankAccounts.First().Id), loginResponse, "sessionCookie");
            HttpResponseMessage responseMessage = await _unAuthenticatedServer
                .CreateClient().SendAsync(requestMessage);

            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
