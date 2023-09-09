using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Application.UrlApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class ProjectsControllerTests : IntegrationTestBase
    {
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedProjects);
        }

        [Test, Order(3)]
        public async Task Should_Get_Not_Completed_Projects()
        {
            // Arrange
            IEnumerable<ProjectDto> expectedProjects = ProjectDtoData.NotCompletedProjectsForStaff;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.ProjectsController.Get());

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedProjects);
        }

        [Test, Order(4)]
        public async Task Should_Get_By_Id()
        {
            // Arrange
            ProjectDto expectedProject = ProjectDtoData.HoorayForHollywood;

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_performer)
                .GetAsync(ApiEndpoints.ProjectsController.Get(expectedProject.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedProject);
        }

        [Test, Order(5)]
        public async Task Should_Get_Participations_By_Id()
        {
            // Arrange
            Project project = ProjectSeedData.RockingXMas;
            IEnumerable<ProjectParticipationDto> expectedResult = new List<ProjectParticipationDto> {
                ProjectParticipationDtoData.AdminRockingXMasParticipation,
                ProjectParticipationDtoData.PerformerRockingXMasParticipationForStaff,
                ProjectParticipationDtoData.StaffRockingXMasBassParticipation,
                ProjectParticipationDtoData.StaffRockingXMasTenorParticipation,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .GetAsync(ApiEndpoints.ProjectsController.GetParticipations(project.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            IEnumerable<ProjectParticipationDto> result = await DeserializeResponseMessageAsync<IEnumerable<ProjectParticipationDto>>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedResult, opt => opt.WithStrictOrderingFor(dto => dto.Id));
        }

        [Test, Order(10000)]
        public async Task Should_Modify()
        {
            // Arrange
            Project projectToModify = ProjectSeedData.HoorayForHollywood;
            HttpClient client = _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff);

            var modifyDto = new ProjectModifyBodyDto
            {
                Title = "changed " + projectToModify.Title,
                ShortTitle = "changed " + projectToModify.ShortTitle,
                Description = "changed " + projectToModify.Description,
                Code = "X-" + projectToModify.Code,
                TypeId = SelectValueMappingSeedData.ProjectTypeMappings[2].Id,
                GenreId = SelectValueMappingSeedData.ProjectGenreMappings[2].Id,
                StartDate = new DateTime(2021, 02, 02),
                EndDate = new DateTime(2021, 02, 28),
                Status = ProjectStatus.Cancelled,
                ParentId = ProjectSeedData.RockingXMas.Id,
                IsHiddenForPerformers = true
            };

            var expectedDto = new ProjectDto
            {
                Description = modifyDto.Description,
                EndDate = modifyDto.EndDate,
                StartDate = modifyDto.StartDate,
                Code = modifyDto.Code,
                ShortTitle = modifyDto.ShortTitle,
                ParentId = modifyDto.ParentId,
                Id = projectToModify.Id,
                Title = modifyDto.Title,
                ModifiedAt = FakeDateTime.UtcNow,
                ModifiedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "anonymous",
                Type = SelectValueDtoData.Workshop,
                Genre = SelectValueDtoData.ChamberMusic,
                Status = ProjectStatus.Cancelled,
                IsHiddenForPerformers = true
            };

            // Act
            HttpResponseMessage responseMessage = await client
                .PutAsync(ApiEndpoints.ProjectsController.Put(projectToModify.Id), BuildStringContent(modifyDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // check now, if modification really did make it into the database. Fetch the (now modified) project again via GetById

            // Act
            responseMessage = await client
                .GetAsync(ApiEndpoints.ProjectsController.Get(projectToModify.Id));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Urls));
        }

        [Test, Order(100)]
        public async Task Should_Create_With_Minimum_Fields_Defined()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                Code = "123XYZ,",
            };

            var expectedDto = new ProjectDto
            {
                Title = createDto.Title,
                ShortTitle = createDto.ShortTitle,
                Code = createDto.Code,
                IsCompleted = false,
                CreatedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);

            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id));
            _ = result.Id.Should().NotBeEmpty();
            _ = responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.ProjectsController.Get(result.Id)}");
        }

        [Test, Order(101)]
        public async Task Should_Create_With_All_Fields_Defined()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "ShortName",
                Description = "New project description",
                Code = "0815XY",
                TypeId = SelectValueMappingSeedData.ProjectTypeMappings[0].Id,
                GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 31),
                Status = ProjectStatus.Pending,
                ParentId = ProjectSeedData.HoorayForHollywood.Id,
                IsHiddenForPerformers = true,
                IsCompleted = true
            };

            var expectedDto = new ProjectDto
            {
                Title = createDto.Title,
                ShortTitle = createDto.ShortTitle,
                Description = createDto.Description,
                Code = createDto.Code,
                Type = SelectValueDtoData.Concert,
                Genre = SelectValueDtoData.ClassicalMusic,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                Status = ProjectStatus.Pending,
                ParentId = createDto.ParentId,
                IsCompleted = true,
                CreatedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow,
                IsHiddenForPerformers = true
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            ProjectDto result = await DeserializeResponseMessageAsync<ProjectDto>(responseMessage);

            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id));
            _ = result.Id.Should().NotBeEmpty();
            _ = responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.ProjectsController.Get(result.Id)}");
        }

        [Test, Order(102)]
        public async Task Should_Not_Create_Due_To_Non_Unique_ProjectCode()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                Code = ProjectSeedData.RockingXMas.Code,
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            _ = errorMessage.Title.Should().Be("One or more validation errors occurred.");
            _ = errorMessage.Status.Should().Be(422);
            _ = errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "Code", new[] { "The specified project code is already in use. The project code needs to be unique." } } });
        }

        [Test, Order(103)]
        public async Task Should_Not_Create_Due_To_Missing_Mandatory_Field()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                // mandatory "Code" field is missing here -> this should create the failure of the api call
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails validationProblemDetails = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            _ = validationProblemDetails.Title.Should().Be("One or more validation errors occurred.");
            _ = validationProblemDetails.Type.Should().Be("https://tools.ietf.org/html/rfc4918#section-11.2");
            _ = validationProblemDetails.Status.Should().Be(422);
            _ = validationProblemDetails.Errors["Code"].Should().NotBeEmpty();
        }

        [Test, Order(104)]
        public async Task Should_Not_Create_Due_Wrong_Dates()
        {
            // Arrange
            var createDto = new ProjectCreateDto
            {
                Title = "New Project",
                ShortTitle = "Shorty",
                Code = "123XYZ,",
                StartDate = new DateTime(2020, 01, 01),
                EndDate = new DateTime(2020, 01, 01) - new TimeSpan(5, 0, 0, 0),    // this is before the StartDate
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.Post(), BuildStringContent(createDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ValidationProblemDetails validationProblemDetails = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            _ = validationProblemDetails.Title.Should().Be("One or more validation errors occurred.");
            _ = validationProblemDetails.Type.Should().Be("https://tools.ietf.org/html/rfc4918#section-11.2");
            _ = validationProblemDetails.Status.Should().Be(422);
            _ = validationProblemDetails.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "EndDate", new[] { "'EndDate' must be greater than 'StartDate'" } } });
        }

        [Test, Order(105)]
        public async Task Should_Add_Url()
        {
            // Arrange
            var urlCreateDto = new UrlCreateBodyDto
            {
                Href = "http://www.landesblasorchester.de",
                AnchorText = "Landesblasorchester Baden-Württemberg"
            };
            var expectedDto = new UrlDto()
            {
                Href = urlCreateDto.Href,
                AnchorText = urlCreateDto.AnchorText,
                CreatedBy = _staff.DisplayName,
                CreatedAt = FakeDateTime.UtcNow
            };

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PostAsync(ApiEndpoints.ProjectsController.AddUrl(ProjectDtoData.HoorayForHollywood.Id), BuildStringContent(urlCreateDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            UrlDto result = await DeserializeResponseMessageAsync<UrlDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.Id));
            _ = result.Id.Should().NotBeEmpty();
            _ = responseMessage.Headers.Location.AbsolutePath.Should().Be($"/{ApiEndpoints.UrlsController.Get(result.Id)}");
        }

        [Test, Order(106)]
        public async Task Should_Not_Add_Url_Due_To_Project_Not_Found()
        {
            // Arrange
            var urlCreateDto = new UrlCreateBodyDto
            {
                Href = "http://www.landesblasorchester.de",
                AnchorText = "Landesblasorchester Baden-Württemberg"
            };

            // Act
            HttpClient client = _authenticatedServer.CreateClient().AuthenticateWith(_staff);
            HttpResponseMessage responseMessage = await client
                .PostAsync(ApiEndpoints.ProjectsController.AddUrl(Guid.NewGuid()), BuildStringContent(urlCreateDto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
            ValidationProblemDetails errorMessage = await DeserializeResponseMessageAsync<ValidationProblemDetails>(responseMessage);
            _ = errorMessage.Title.Should().Be("Resource not found.");
            _ = errorMessage.Status.Should().Be(404);
            _ = errorMessage.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>() { { "ProjectId", new[] { "Project could not be found." } } });
        }

        [Test, Order(107)]
        public async Task Should_Set_New_Project_Participation()
        {
            // Arrange
            MusicianProfile musicianProfile = MusicianProfileSeedData.PerformersHornMusicianProfile;
            Project project = ProjectSeedData.ChorwerkstattBerlin;
            var dto = new SetProjectParticipationBodyDto
            {
                MusicianProfileId = musicianProfile.Id,
                CommentByStaffInner = "Staff comment",
                CommentTeam = "Team comment",
                InvitationStatus = ProjectInvitationStatus.Invited,
                ParticipationStatusInner = ProjectParticipationStatusInner.Acceptance,
                ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate,
            };
            var expectedDto = new ProjectParticipationDto
            {
                ParticipationStatusInner = dto.ParticipationStatusInner,
                ParticipationStatusInternal = dto.ParticipationStatusInternal,
                InvitationStatus = dto.InvitationStatus,
                CreatedAt = FakeDateTime.UtcNow,
                CreatedBy = "Staff Member",
                CommentByStaffInner = "Staff comment",
                CommentTeam = "Team comment",
                MusicianProfile = ReducedMusicianProfileDtoData.PerformerHornProfile,
                Project = ReducedProjectDtoData.ChorwerkstattBerlin,
                Person = ReducedPersonDtoData.Performer
            };
            _fakeSmtpServer.ClearReceivedEmail();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.ProjectsController.SetParticipation(project.Id), BuildStringContent(dto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ProjectParticipationDto result = await DeserializeResponseMessageAsync<ProjectParticipationDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
            _ = result.Id.Should().NotBeEmpty();
            EvaluateSimpleEmail("Dein Teilnahmestatus für 1004 - Chorwerkstatt Berlin wurde aktualisiert!", _performer.Email);
        }

        [Test, Order(108)]
        public async Task Should_Set_Existing_Project_Participation()
        {
            // Arrange
            MusicianProfile musicianProfile = MusicianProfileSeedData.PerformerMusicianProfile;
            Project project = ProjectSeedData.Schneekönigin;

            var dto = new SetProjectParticipationBodyDto
            {
                MusicianProfileId = musicianProfile.Id,
                CommentByStaffInner = "Staff comment",
                CommentTeam = "Team comment",
                InvitationStatus = ProjectInvitationStatus.NotInvited,
                ParticipationStatusInner = ProjectParticipationStatusInner.Pending,
                ParticipationStatusInternal = ProjectParticipationStatusInternal.Pending
            };
            ProjectParticipationDto expectedDto = ProjectParticipationDtoData.PerformerSchneeköniginParticipationForStaff;
            expectedDto.CommentByStaffInner = dto.CommentByStaffInner;
            expectedDto.CommentTeam = dto.CommentTeam;
            expectedDto.InvitationStatus = dto.InvitationStatus;
            expectedDto.ParticipationStatusInner = dto.ParticipationStatusInner.Value;
            expectedDto.ParticipationStatusInternal = dto.ParticipationStatusInternal;
            expectedDto.ParticipationStatusResult = ProjectParticipationStatusResult.Pending;
            expectedDto.ModifiedAt = FakeDateTime.UtcNow;
            expectedDto.ModifiedBy = "Staff Member";
            _fakeSmtpServer.ClearReceivedEmail();

            // Act
            HttpResponseMessage responseMessage = await _authenticatedServer
                .CreateClient()
                .AuthenticateWith(_staff)
                .PutAsync(ApiEndpoints.ProjectsController.SetParticipation(project.Id), BuildStringContent(dto));

            // Assert
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            ProjectParticipationDto result = await DeserializeResponseMessageAsync<ProjectParticipationDto>(responseMessage);
            _ = result.Should().BeEquivalentTo(expectedDto);
            EvaluateSimpleEmail("Dein Teilnahmestatus für 1007 - Die Schneekönigin wurde aktualisiert!", _performer.Email);
        }

        [Test, Order(10000)]
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
            _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
