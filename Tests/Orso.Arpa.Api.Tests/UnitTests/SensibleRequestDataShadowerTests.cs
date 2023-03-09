using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Api.Tests.UnitTests
{
    [TestFixture]
    public class SensibleRequestDataShadowerTests
    {
        [Test]
        public async Task Should_Shadow_Sensible_Request_Data_For_Logging()
        {
            var dto = new UserRegisterDto
            {
                DateOfBirth = FakeDateTime.UtcNow,
                ClientUri = "my/client/uri",
                Email = "my@test.mail",
                GenderId = Guid.Parse("d69ed12f-bb0c-4577-8432-8460bfceb7d6"),
                GivenName = "Givennamewithäü",
                Password = "PasswordToBeShadowed",
                Surname = "Surname",
                UserName = "Username"
            };
            dto.StakeholderGroupIds.Add(Guid.Parse("fd8a2aea-42ee-4718-99d0-c759b89feb48"));
            var jso = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            var json = JsonSerializer.Serialize(dto, jso);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            const string expectedResult = "{\r\n  \"UserName\": \"Username\",\r\n  \"Password\": \"**********\",\r\n  \"Email\": \"my@test.mail\",\r\n  \"GivenName\": \"Givennamewithäü\",\r\n  \"Surname\": \"Surname\",\r\n  \"GenderId\": \"d69ed12f-bb0c-4577-8432-8460bfceb7d6\",\r\n  \"DateOfBirth\": \"2030-02-02T00:00:00\",\r\n  \"ClientUri\": \"my/client/uri\",\r\n  \"StakeholderGroupIds\": [\r\n    \"fd8a2aea-42ee-4718-99d0-c759b89feb48\"\r\n  ]\r\n}";

            var result = await SensibleRequestDataShadower.ShadowSensibleDataForLoggingAsync(stream);

            _ = result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
