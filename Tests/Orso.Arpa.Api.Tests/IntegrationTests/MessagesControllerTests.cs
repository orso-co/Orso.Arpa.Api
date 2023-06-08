using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MessageApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class MessagesControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_All()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MessagesController.Get());

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<MessageDto> result = await DeserializeResponseMessageAsync<IEnumerable<MessageDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(MessageDtoData.Messages, opt => opt.WithStrictOrdering());
        }

        [Test, Order(2)]
        public async Task Should_Get_ById()
        {
            // Arrange
            MessageDto expectedDto = MessageDtoData.Performer;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MessagesController.Get(expectedDto.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
           MessageDto result = await DeserializeResponseMessageAsync<MessageDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
        }
        [Test, Order(100)]
        public async Task Should_Modify()
        {
            // Arrange
            Message messageToModify = MessageSeedData.ErsteMessage;
            var modifyDto = new MessageModifyBodyDto
            {
                MessageText = "ErsteMessage",
                Url = "http://orsopolis.de",
                Show = true,
            };

            var expectedDto = new MessageDto
            {
                Id = messageToModify.Id,
                MessageText = "ErsteMessageModifiziert",
                Url = "http://orsopolis.com",
                Show = false,
            };

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.MessagesController.Put(messageToModify.Id), BuildStringContent(modifyDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.MessagesController.Get(messageToModify.Id));

            _ = getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MessageDto result = await DeserializeResponseMessageAsync<MessageDto>(getMessage);

            _ = result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(10000)]
        public async Task Should_Delete()
        {
            // Arrange

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.MessagesController.Delete(MessageSeedData.ZweiteMessage.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
