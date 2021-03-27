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
using Orso.Arpa.Persistence.Seed;
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
                .GetAsync(ApiEndpoints.ProjectsController.Get(true));           // get all projects, including completed projects

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectDto>>(responseMessage);
            result.Should().BeEquivalentTo(ProjectDtoData.Projects);
        }

        [Test, Order(2)]
        public async Task Should_Get_Only_Completed_Projects()
        {
            // Arrange
            IList<ProjectDto> expectedProjects = new List<ProjectDto>();
            foreach (ProjectDto p in ProjectDtoData.Projects)                   // collect all projects that are NOT completed
            {
                if (!p.IsCompleted)
                    expectedProjects.Add(p);
            }

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.ProjectsController.Get());               // get only NOT completed projects

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectDto>>(responseMessage);
            result.Should().BeEquivalentTo(expectedProjects);
        }

        [Test, Order(3)]
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
                Number = "123XYZ,",
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
                TypeId = SelectValueMappingSeedData.ProjectTypeMappings[0].Id,
                GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 31),
                Urls = new List<UrlDto>
                {
                    UrlDtoData.ArpaWebsite,
                    UrlDtoData.OrsoWebsite,
                },

                StateId = SelectValueMappingSeedData.ProjectStateMappings[0].Id,
                ParentId = ProjectSeedData.HoorayForHollywood.Id,
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
                Urls = createDto.Urls,
                StateId = createDto.StateId,
                ParentId = createDto.ParentId,
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
        public async Task Should_Not_Create_Due_To_Non_Unique_Number()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                Number = ProjectSeedData.HoorayForHollywood.Number,
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
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test, Order(1003)]
        public async Task Should_Not_Create_Due_To_Missing_Mandatory_Field()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                // mandatory "Number" field is missing here -> this should create the failure of the api call
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
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test, Order(1004)]
        public async Task Should_Not_Create_Due_Wrong_Dates()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                Number = "123XYZ,",
                StartDate = new DateTime(2020, 01, 01),
                EndDate = new DateTime(2020, 01, 01) - new TimeSpan(5, 0, 0, 0),    // this is before the StartDate
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test, Order(10100)]
        public async Task Should_Modify()
        {
            // Arrange
            Project projectToModify = ProjectSeedData.HoorayForHollywood;

            var modifyDto = new ProjectModifyDto
            {
                Title = "changed " + projectToModify.Title,
                ShortTitle = "changed " + projectToModify.ShortTitle,
                Description = "changed " + projectToModify.Description,
                Number = "X-" + projectToModify.Number,
                TypeId = SelectValueMappingSeedData.ProjectTypeMappings[2].Id,
                GenreId = SelectValueMappingSeedData.ProjectGenreMappings[2].Id,
                StartDate = new DateTime(2021, 02, 02),
                EndDate = new DateTime(2021, 02, 28),
                StateId = SelectValueMappingSeedData.ProjectStateMappings[2].Id,
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

        [Test, Order(10500)]
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
