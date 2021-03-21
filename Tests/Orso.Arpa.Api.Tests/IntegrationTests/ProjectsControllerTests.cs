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
        public async Task Should_Create()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                Description = "New project description",
            };

            var expectedDto = new ProjectDto
            {
                Title = createDto.Title,
                Description = createDto.Description,
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

        [Test, Order(107)]
        public async Task Should_Modify()
        {
            // Arrange
            Project projectToModify = ProjectSeedData.HoorayForHollywood;

            var modifyDto = new ProjectModifyDto
            {
                Title = "changed" + projectToModify.Title,
                Description = "changed" + projectToModify.Description,
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
            responseMessage= await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.ProjectsController.Get(projectToModify.Id));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);
            result.Should().BeEquivalentTo(modifyDto, opt => opt.Excluding(r => r.Id));
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
