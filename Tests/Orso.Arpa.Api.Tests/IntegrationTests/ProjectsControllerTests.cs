using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Misc;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class ProjectsControllerTests : IntegrationTestBase
    {
        [Test, Order(1)]
        public async Task Should_Get_Projects()
        {
            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.ProjectsController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectDto>>(responseMessage);
            result.Should().BeEquivalentTo(ProjectDtoData.Projects);
        }

        [Test, Order(2)]
        public async Task Should_Get_By_Id()
        {
            using var context = new DateTimeProviderContext(new DateTime(2021, 1, 1));
            ProjectDto expectedProject = ProjectDtoData.HoorayForHollywood;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.ProjectsController.Get(expectedProject.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedProject);
        }

        [Test, Order(1000)]
        public async Task Should_Create_With_Minimum_Fields_Defined()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                Number = "123XYZ,"
            };

            var expectedDto = new ProjectDto
            {
                Title = createDto.Title,
                ShortTitle = createDto.ShortTitle,
                Number = createDto.Number,
                IsCompleted = false,
                CreatedBy = _staff.DisplayName,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.CreatedAt).Excluding(r => r.Id));
            result.Id.Should().NotBeEmpty();
            result.CreatedAt.Should().BeBefore(DateTime.UtcNow);
            result.CreatedAt.Should().BeAfter(DateTime.MinValue);
        }

        [Test, Order(1001)]
        public async Task Should_Create_With_All_Fields_Defined()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "ShortName",
                Description = "New project description",
                Number = "0815XY",
                //TODO TypeId = ,
                //TODO GenreId = ,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 31),
                //TODO Urls =,
                //TODO StateId = ,
                //TODO ParentId = ,
            };

            var expectedDto = new ProjectDto
            {
                Title = createDto.Title,
                ShortTitle = createDto.ShortTitle,
                Description = createDto.Description,
                Number = createDto.Number,
                TypeId = createDto.TypeId,
                GenreId = createDto.GenreId,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                //TODO Urls =
                //TODO StateId =
                //TODO ParentId =
                IsCompleted = false,
                CreatedBy = _staff.DisplayName,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.CreatedAt).Excluding(r => r.Id));
            result.Id.Should().NotBeEmpty();
            result.CreatedAt.Should().BeBefore(DateTime.UtcNow);
            result.CreatedAt.Should().BeAfter(DateTime.MinValue);
        }

        [Test, Order(1002)]
        public async Task Should_Not_Create_Due_To_Non_unique_Number()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                Number = "123XYZ,"
            };

            var expectedDto = new ProjectDto
            {
                Title = createDto.Title,
                ShortTitle = createDto.ShortTitle,
                Number = createDto.Number,
                IsCompleted = false,
                CreatedBy = _staff.DisplayName,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.CreatedAt).Excluding(r => r.Id));
            result.Id.Should().BeEmpty();   // TODO how to detect failure of creation?
            result.CreatedAt.Should().BeBefore(DateTime.UtcNow);
            result.CreatedAt.Should().BeAfter(DateTime.MinValue);
        }

        [Test, Order(1003)]
        public async Task Should_Not_Create_Due_To_Missing_Mandatory_Field()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
            };

            var expectedDto = new ProjectDto
            {
                Title = createDto.Title,
                ShortTitle = createDto.ShortTitle,
                IsCompleted = false,
                CreatedBy = _staff.DisplayName,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);

            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.CreatedAt).Excluding(r => r.Id));
            result.Id.Should().BeEmpty();   // TODO how to detect failure of creation?
            result.CreatedAt.Should().BeBefore(DateTime.UtcNow);
            result.CreatedAt.Should().BeAfter(DateTime.MinValue);
        }

        [Test, Order(107)]
        public async Task Should_Modify()
        {
            // Arrange
            Project projectToModify = ProjectSeedData.HoorayForHollywood;

            var modifyDto = new ProjectModifyDto
            {
                Title = "changed " + projectToModify.Title,
                ShortTitle = "changed " + projectToModify.ShortTitle,
                Description = "changed " + projectToModify.Description,
                Number = "X_" + projectToModify.Number,
                //TODO TypeId = other ID
                //TODO GenreId = other ID
                StartDate = new DateTime(2021, 02, 02),
                EndDate = new DateTime(2021, 02, 28),
                //TODO Urls =
                //TODO StateId =
                //TODO ParentId =
                IsCompleted = true,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.ProjectsController.Put(projectToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // check now, if modification really did make it into the database. Fetch the (now modified) project again via GetById

            // Act
            responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.ProjectsController.Get(projectToModify.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);
            result.Should().BeEquivalentTo(modifyDto, opt => opt.Excluding(r => r.Id));
            result.ModifiedBy.Should().Be(_staff.DisplayName);
            result.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
            result.ModifiedAt.Should().BeAfter(DateTime.MinValue);
        }

        [Test, Order(10004)]
        public async Task Should_Delete()
        {
            // Arrange
            Project projectToDelete = ProjectSeedData.HoorayForHollywood;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .DeleteAsync(ApiEndpoints.ProjectsController.Delete(projectToDelete.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
