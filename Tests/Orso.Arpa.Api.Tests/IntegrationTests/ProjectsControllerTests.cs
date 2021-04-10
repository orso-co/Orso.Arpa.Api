using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Application.UrlApplication;
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
        public async Task Should_Get_All_Projects_As_Performer()
        {
            // Arrange
            IList<ProjectDto> expectedProjects = ProjectDtoData.ProjectsForPerformer;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.ProjectsController.Get(true));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectDto>>(responseMessage);
            result.Should().BeEquivalentTo(expectedProjects);
        }

        [Test, Order(2)]
        public async Task Should_Get_All_Projects_As_Staff()
        {
            // Arrange
            IList<ProjectDto> expectedProjects = ProjectDtoData.ProjectsForStaff;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.ProjectsController.Get(true));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectDto>>(responseMessage);
            result.Should().BeEquivalentTo(expectedProjects);
        }

        [Test, Order(3)]
        public async Task Should_Get_Only_Completed_Projects()
        {
            // Arrange
            IEnumerable<ProjectDto> expectedProjects = ProjectDtoData.ProjectsForPerformer.Where(p => !p.IsCompleted);

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.ProjectsController.Get());

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectDto>>(responseMessage);
            result.Should().BeEquivalentTo(expectedProjects);
        }

        [Test, Order(4)]
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

        [Test, Order(10)]
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

        [Test, Order(11)]
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

        [Test, Order(12)]
        public async Task Should_Not_Create_Due_To_Non_Unique_Number()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                Number = ProjectSeedData.HoorayForHollywood.Number,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test, Order(13)]
        public async Task Should_Not_Create_Due_To_Missing_Mandatory_Field()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                // mandatory "Number" field is missing here -> this should create the failure of the api call
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test, Order(14)]
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

        [Test, Order(30)]
        public async Task Should_Modify()
        {
            // Arrange
            Project projectToModify = ProjectSeedData.HoorayForHollywood;
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

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
                ParentId = ProjectSeedData.RockingXMas.Id,
                IsCompleted = true,
            };

            // Act
            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.ProjectsController.Put(projectToModify.Id), BuildStringContent(modifyDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // check now, if modification really did make it into the database. Fetch the (now modified) project again via GetById

            // Act
            responseMessage = await client
                .GetAsync(ApiEndpoints.ProjectsController.Get(projectToModify.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);
            result.Should().BeEquivalentTo(modifyDto, opt => opt.Excluding(r => r.Id));
            result.Id.Should().Be(projectToModify.Id);
            result.ModifiedBy.Should().Be(_staff.DisplayName);
            result.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
            result.ModifiedAt.Should().BeAfter(DateTime.MinValue);
        }

        [Test, Order(100)]
        public async Task Should_Delete()
        {
            // Arrange
            Project projectToDelete = ProjectSeedData.HoorayForHollywood;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_admin)
                .DeleteAsync(ApiEndpoints.ProjectsController.Delete(projectToDelete.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(20)]
        public async Task Should_Add_Url()
        {
            // Arrange
            var addDto = new ProjectAddUrlDto
            {
                Href = "http://www.landesblasorchester.de",
                AnchorText = "Landesblasorchester Baden-Württemberg",
                ProjectId = ProjectDtoData.HoorayForHollywood.Id,
            };
            var expectedDto = new UrlDto()
            {
                Href = addDto.Href,
                AnchorText = addDto.AnchorText,
                CreatedBy = _staff.DisplayName,
            };

            // Act
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);
            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.ProjectsController.Post(expectedDto.Id), BuildStringContent(addDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            UrlDto result = await DeserializeResponseMessageAsync<UrlDto>(responseMessage);
            result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id).Excluding(r => r.CreatedAt));
        }

        [Test, Order(21)]
        public async Task Should_Not_Add_Url_Due_To_Project_Not_Found()
        {
            // Arrange
            var addDto = new ProjectAddUrlDto
            {
                Href = "http://www.landesblasorchester.de",
                AnchorText = "Landesblasorchester Baden-Württemberg",
                ProjectId = Guid.NewGuid(),
            };

            // Act
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);
            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.ProjectsController.Post(addDto.ProjectId), BuildStringContent(addDto));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
