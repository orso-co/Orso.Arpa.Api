using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.PersonApplication;
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
        public async Task Should_Get_My_UserProfile()
        {
            // Arrange
            MyUserProfileDto expectedDto = UserProfileDtoData.Performer;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MeController.GetUserProfile());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyUserProfileDto result = await DeserializeResponseMessageAsync<MyUserProfileDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        private static IEnumerable<TestCaseData> s_appointmentTestData
        {
            get
            {
                yield return new TestCaseData(null, null, UserAppointmentDtoTestData.PerformerUserAppointments);
                yield return new TestCaseData(1, 2, new List<MyAppointmentDto>
                {
                    UserAppointmentDtoTestData.RockingXMasAfterShowParty,
                    UserAppointmentDtoTestData.RockingXMasConcert,
                });
            }
        }

        [Test, Order(2)]
        [TestCaseSource(nameof(s_appointmentTestData))]
        public async Task Should_Get_My_Appointments(int? skip, int? take, IEnumerable<MyAppointmentDto> expectedResult)
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MeController.GetAppointments(take, skip, true));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyAppointmentListDto result = await DeserializeResponseMessageAsync<MyAppointmentListDto>(responseMessage);

            result.UserAppointments.Should().BeEquivalentTo(expectedResult, opt => opt.WithStrictOrderingFor(d => d.Id));
            result.TotalRecordsCount.Should().Be(4);
        }

        [Test, Order(3)]
        public async Task Should_Send_QRCode()
        {
            // Arrange
            var expectedFile = File.ReadAllBytes(
                Path.Combine(AppContext.BaseDirectory,
                "Files",
                "ARPA_QRCode_Per_Former.png"));

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.MeController.GetQrCode(true));

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
                .GetAsync(ApiEndpoints.MeController.GetQrCode(true));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test, Order(100)]
        public async Task Should_Modify_My_UserProfile()
        {
            // Arrange
            User userToModify = FakeUsers.Performer;
            var modifyDto = new MyUserProfileModifyDto
            {
                Email = "changed" + userToModify.Email,
                GivenName = "changed" + userToModify.Person.GivenName,
                Surname = "changed" + userToModify.Person.Surname,
                DateOfBirth = userToModify.Person.DateOfBirth.Value.AddDays(1),
                AboutMe = "changed" + userToModify.Person.AboutMe,
                BirthName = "changed" + userToModify.Person.BirthName,
                Birthplace = "changed" + userToModify.Person.Birthplace,
                GenderId = SelectValueMappingSeedData.PersonGenderMappings[0].Id
            };

            PersonDto sourcePerson = PersonDtoData.PerformerForNonStaff;
            var expectedDto = new MyUserProfileDto
            {
                Email = modifyDto.Email,
                UserName = userToModify.UserName,
                Person = new PersonDto
                {
                    DateOfBirth = modifyDto.DateOfBirth,
                    ContactDetails = sourcePerson.ContactDetails,
                    AboutMe = modifyDto.AboutMe,
                    Addresses = sourcePerson.Addresses,
                    BankAccounts = sourcePerson.BankAccounts,
                    BirthName = modifyDto.BirthName,
                    Birthplace = modifyDto.Birthplace,
                    ContactsRecommended = sourcePerson.ContactsRecommended,
                    Surname = modifyDto.Surname,
                    GivenName = modifyDto.GivenName,
                    Id = userToModify.PersonId,
                    ModifiedAt = FakeDateTime.UtcNow,
                    ModifiedBy = "Per Former",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Gender = SelectValueDtoData.Female,
                    StakeholderGroups = sourcePerson.StakeholderGroups
                }
            };

            // Act
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.MeController.PutUserProfile(), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.MeController.GetUserProfile());

            getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            MyUserProfileDto result = await DeserializeResponseMessageAsync<MyUserProfileDto>(getMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(1000)]
        public async Task Should_Set_Appointment_Participation_Prediction()
        {
            // Arrange
            var dto = new SetMyAppointmentParticipationPredictionBodyDto
            {
                CommentByPerformerInner = "CommentByPerformerInner"
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.MeController.SetAppointmentParticipationPrediction(
                    AppointmentSeedData.RockingXMasRehearsal.Id,
                    SelectValueMappingSeedData.AppointmentParticipationPredictionMappings[0].Id),
                    BuildStringContent(dto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
