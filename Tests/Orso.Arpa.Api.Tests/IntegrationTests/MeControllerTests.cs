using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class MeControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_My_Profile()
        {
            // Arrange
            MyProfileDto expectedDto = UserProfileDtoData.Performer;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MeController.GetProfile());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyProfileDto result = await DeserializeResponseMessageAsync<MyProfileDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(2)]
        public async Task Should_Get_My_Appointments()
        {
            // Arrange
            IList<MyAppointmentDto> expectedUserAppointments = UserAppointmentDtoTestData.PerformerUserAppointments;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MeController.GetAppointments(1, 1));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyAppointmentListDto result = await DeserializeResponseMessageAsync<MyAppointmentListDto>(responseMessage);

            result.UserAppointments.Should().BeEquivalentTo(expectedUserAppointments);
            result.TotalRecordsCount.Should().Be(2);
        }

        [Test, Order(3)]
        public async Task Should_Send_QRCode()
        {
            // Arrange
            var expectedFile = File.ReadAllBytes(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Files",
                "ARPA_QRCode_Per_Former.png"));

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MeController.SendQrCode());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            responseMessage.Content.Headers.ContentType.MediaType.Should().BeEquivalentTo("image/png");
            responseMessage.Content.Headers.ContentDisposition.FileName.Should().Be("ARPA_QRCode_Per_Former.png");
            byte[] responseContent = await responseMessage.Content.ReadAsByteArrayAsync();
            responseContent.Should().NotBeEmpty();
            responseContent.Should().BeEquivalentTo(expectedFile);
            _fakeSmtpServer.ReceivedEmailCount.Should().Be(1);
            netDumbster.smtp.SmtpMessage receivedMail = _fakeSmtpServer.ReceivedEmail[0];
            receivedMail.ToAddresses[0].Address.Should().ContainEquivalentOf(_performer.Email);
            receivedMail.MessageParts.Length.Should().Be(2);
        }

        [Test, Order(4)]
        public async Task Should_Not_Send_QRCode_If_User_Is_Not_In_Role_Performer()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_admin)
                .GetAsync(ApiEndpoints.MeController.SendQrCode());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test, Order(1000)]
        public async Task Should_Modify_Profile()
        {
            // Arrange
            User userToModify = FakeUsers.Performer;
            var modifyDto = new MyProfileModifyDto
            {
                Email = "changed" + userToModify.Email,
                GivenName = "changed" + userToModify.Person.GivenName,
                Surname = "changed" + userToModify.Person.Surname,
                PhoneNumber = "changed" + userToModify.PhoneNumber
            };

            var expectedDto = new MyProfileDto
            {
                Email = modifyDto.Email,
                GivenName = modifyDto.GivenName,
                PhoneNumber = modifyDto.PhoneNumber,
                Surname = modifyDto.Surname,
                UserName = userToModify.UserName
            };

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.MeController.PutProfile(), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.MeController.GetProfile());

            getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyProfileDto result = await DeserializeResponseMessageAsync<MyProfileDto>(getMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(1001)]
        public async Task Should_Set_Appointment_Participation_Prediction()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.MeController.SetAppointmentParticipationPrediction(
                    AppointmentSeedData.RockingXMasRehearsal.Id,
                    SelectValueMappingSeedData.AppointmentParticipationPredictionMappings[0].Id), null);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
