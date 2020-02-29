using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.Logic.Me;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    public class MeControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_My_Profile()
        {
            // Arrange
            UserProfileDto expectedDto = UserProfileDtoData.Orsianer;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .GetAsync(ApiEndpoints.MeController.GetProfile());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            UserProfileDto result = await DeserializeResponseMessageAsync<UserProfileDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test, Order(2)]
        public async Task Should_Get_My_Appointments()
        {
            // Arrange
            UserAppointmentDto expectedUserAppointment = UserAppointmentDtoTestData.OrsianerUserAppointment;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_orsianer)
                .GetAsync(ApiEndpoints.MeController.GetAppointments(1, 1));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<UserAppointmentDto> result = await DeserializeResponseMessageAsync<IEnumerable<UserAppointmentDto>>(responseMessage);

            result.Should().BeEquivalentTo(UserAppointmentDtoTestData.OrsianerUserAppointments, opt => opt
                .Excluding(dto => dto.CreatedAt)
                .Excluding(dto => dto.Projects)
                .Excluding(dto => dto.Venue));
            UserAppointmentDto userAppointment = result.First();
            userAppointment.Venue.Should().BeEquivalentTo(expectedUserAppointment.Venue, opt => opt
                .Excluding(dto => dto.CreatedAt)
                .Excluding(dto => dto.Rooms));
            userAppointment.Venue.Rooms.Should().BeEquivalentTo(expectedUserAppointment.Venue.Rooms, opt => opt
                .Excluding(dto => dto.CreatedAt));
            userAppointment.Projects.Should().BeEquivalentTo(expectedUserAppointment.Projects, opt => opt
                .Excluding(dto => dto.CreatedAt));
        }

        [Test, Order(1000)]
        public async Task Should_Modify_Profile()
        {
            // Arrange
            User userToModify = FakeUsers.Orsianer;
            var modifyDto = new Modify.Dto
            {
                Email = "changed" + userToModify.Email,
                GivenName = "changed" + userToModify.Person.GivenName,
                Surname = "changed" + userToModify.Person.Surname,
                PhoneNumber = "changed" + userToModify.PhoneNumber
            };

            var expectedDto = new UserProfileDto
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
                .AuthenticateWith(_orsianer);

            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.MeController.PutProfile(), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            HttpResponseMessage getMessage = await client
                .GetAsync(ApiEndpoints.MeController.GetProfile());

            getMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            UserProfileDto result = await DeserializeResponseMessageAsync<UserProfileDto>(getMessage);

            result.Should().BeEquivalentTo(expectedDto);
        }
    }
}
