using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.AuditLogApplication.Model;
using Orso.Arpa.Domain.AuditLogDomain.Enums;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class AuditLogsControllerTests : IntegrationTestBase
    {
        private static Guid GetEntryId()
        {
            return JsonSerializer.Deserialize<Dictionary<string, Guid>>(AuditLogSeedData.CreateRegion.KeyValues, (JsonSerializerOptions)null)["Id"];
        }

        [Test, Order(1)]
        public async Task Should_Get_All()
        {
            // Arrange

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.AuditLogsController.Get(null, null, 70));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<AuditLogDto> result = await DeserializeResponseMessageAsync<IEnumerable<AuditLogDto>>(responseMessage);
            result.Count().Should().Be(70);
        }

        [Test, Order(2)]
        public async Task Should_Get_All_ByEntryId()
        {
            // Arrange
            Guid entityId = GetEntryId();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.AuditLogsController.Get(entityId, 0, 9999));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<AuditLogDto> result = await DeserializeResponseMessageAsync<IEnumerable<AuditLogDto>>(responseMessage);
            result.Count().Should().Be(3);

            AuditLogDto auditLog = result.Last();               // this should be the create command as the oldest entry
            auditLog.Type.Should().Be(AuditLogType.Create);     // cannot test this here, because sort order does not work due to test seed data being all created at FakeDateTime.UtcNow which all have the time 00:00:00
            auditLog.NewValues.Count.Should().BeGreaterThan(0); // prove that mapping dictionaries work
        }

        [Test, Order(3)]
        public async Task Should_Get_Page()
        {
            // Arrange
            Guid entityId = GetEntryId();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.AuditLogsController.Get(entityId, 0, 2));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<AuditLogDto> result = await DeserializeResponseMessageAsync<IEnumerable<AuditLogDto>>(responseMessage);
            result.Count().Should().Be(2);
        }

        [Test, Order(4)]
        public async Task Should_Get_Last_Page()
        {
            // Arrange
            Guid entityId = GetEntryId();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.AuditLogsController.Get(entityId, 2, 25));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<AuditLogDto> result = await DeserializeResponseMessageAsync<IEnumerable<AuditLogDto>>(responseMessage);
            result.Count().Should().Be(1);
        }

        [Test, Order(5)]
        public async Task Should_Get_Empty_List()
        {
            // Arrange

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.AuditLogsController.Get(Guid.NewGuid(), 0, 25));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<AuditLogDto> result = await DeserializeResponseMessageAsync<IEnumerable<AuditLogDto>>(responseMessage);
            result.Count().Should().Be(0);
        }
    }
}
