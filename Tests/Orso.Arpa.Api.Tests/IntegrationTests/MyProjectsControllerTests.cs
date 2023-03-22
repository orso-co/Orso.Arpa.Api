using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MyProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests;

public class MyProjectsControllerTests : IntegrationTestBase
{
    [Test, Order(1)]
    public async Task Should_Get_My_Projects()
    {
        // Arrange
        var expectedResult = new MyProjectListDto
        {
            TotalRecordsCount = 3,
            UserProjects = MyProjectDtoData.PerformerProjects
        };

        // Act
        HttpResponseMessage responseMessage = await _authenticatedServer
            .CreateClient()
            .AuthenticateWith(_performer)
            .GetAsync(ApiEndpoints.MyProjectsController.Get());

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        MyProjectListDto result = await DeserializeResponseMessageAsync<MyProjectListDto>(responseMessage);
        _ = result.Should().BeEquivalentTo(expectedResult, opt => opt.WithStrictOrderingFor(dto => dto.UserProjects));
    }

    [Test, Order(2)]
    public async Task Should_Get_My_Projects_With_Pagination_In_German()
    {
        // Arrange
        MyProjectDto dto = MyProjectDtoData.PerformerHoorayForHollywoodDto;
        dto.Project.Type.Name = "Konzertreise (Tour)";
        dto.Participations[0].MusicianProfile.InstrumentName = "Tuba";
        dto.Participations[1].MusicianProfile.InstrumentName = "Alt";
        var expectedResult = new MyProjectListDto
        {
            UserProjects = new List<MyProjectDto>
            {
                dto
            },
            TotalRecordsCount = 3
        };
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, ApiEndpoints.MyProjectsController.Get(offset: 2, limit: 1));
        requestMessage.Headers.Add("Accept-Language", "de");

        // Act
        HttpResponseMessage responseMessage = await _authenticatedServer
            .CreateClient()
            .AuthenticateWith(_performer)
            .SendAsync(requestMessage);

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        MyProjectListDto result = await DeserializeResponseMessageAsync<MyProjectListDto>(responseMessage);
        _ = result.Should().BeEquivalentTo(expectedResult);
    }

    [Test, Order(3)]
    public async Task Should_Get_My_ProjectsIncludeCompleted()
    {
        // Arrange
        var rockingXMasDto = new MyProjectDto
        {
            Project = ProjectDtoData.RockingXMasForPerformer
        };
        rockingXMasDto.Participations.Add(new MyProjectParticipationDto
        {
            MusicianProfile = ReducedMusicianProfileDtoData.PerformerDeactivatedTubaProfile,
            ParticipationStatusResult = ProjectParticipationStatusResult.Pending
        });
        rockingXMasDto.Participations.Add(new MyProjectParticipationDto
        {
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            Id = Guid.Parse("2b3503d3-9061-4110-85e6-88e864842ece"),
            MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
            ParticipationStatusResult = ProjectParticipationStatusResult.Pending
        });
        rockingXMasDto.Participations.Add(new MyProjectParticipationDto
        {
            MusicianProfile = ReducedMusicianProfileDtoData.PerformerHornProfile,
            ParticipationStatusResult = ProjectParticipationStatusResult.Pending
        });
        var expectedList = new List<MyProjectDto>
        {
            rockingXMasDto
        };
        expectedList.AddRange(MyProjectDtoData.PerformerProjects);
        var expectedDto = new MyProjectListDto
        {
            UserProjects = expectedList,
            TotalRecordsCount = 4
        };

        // Act
        HttpResponseMessage responseMessage = await _authenticatedServer
            .CreateClient()
            .AuthenticateWith(_performer)
            .GetAsync(ApiEndpoints.MyProjectsController.Get(includeCompleted: true));

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        MyProjectListDto result = await DeserializeResponseMessageAsync<MyProjectListDto>(responseMessage);
        _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.WithStrictOrderingFor(dto => dto.UserProjects));
    }

    [Test, Order(100)]
    public async Task Should_Set_Existing_Project_Participation()
    {
        // Arrange
        Project project = ProjectSeedData.Schneekönigin;

        var dto = new MyProjectParticipationModifyBodyDto
        {
            ParticipationStatusInner = ProjectParticipationStatusInner.Interested,
            CommentByPerformerInner = "Performer comment",
            MusicianProfileId = MusicianProfileSeedData.PerformerMusicianProfile.Id
        };

        var expectedDto = new MyProjectParticipationDto()
        {
            ParticipationStatusInner = ProjectParticipationStatusInner.Interested,
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            ModifiedAt = FakeDateTime.UtcNow,
            ModifiedBy = "Per Former",
            CommentByPerformerInner = dto.CommentByPerformerInner,
            MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
            CommentByStaffInner = "Comment by staff",
            Id = Guid.Parse("429ac181-9b36-4635-8914-faabc5f593ff"),
            ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate
        };

        // Act
        HttpResponseMessage responseMessage = await _authenticatedServer
            .CreateClient()
            .AuthenticateWith(_performer)
            .PutAsync(ApiEndpoints.MyProjectsController.SetParticipation(project.Id), BuildStringContent(dto));

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        MyProjectParticipationDto result = await DeserializeResponseMessageAsync<MyProjectParticipationDto>(responseMessage);
        _ = result.Should().BeEquivalentTo(expectedDto);
    }

    [Test, Order(101)]
    public async Task Should_Set_New_Project_Participation()
    {
        // Arrange
        Project project = ProjectSeedData.ChorwerkstattBerlin;

        var dto = new MyProjectParticipationModifyBodyDto
        {
            ParticipationStatusInner = ProjectParticipationStatusInner.RehearsalsOnly,
            CommentByPerformerInner = "Performer comment",
            MusicianProfileId = MusicianProfileSeedData.PerformerMusicianProfile.Id
        };

        var expectedDto = new MyProjectParticipationDto()
        {
            ParticipationStatusInner = ProjectParticipationStatusInner.RehearsalsOnly,
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "Per Former",
            CommentByPerformerInner = dto.CommentByPerformerInner,
            MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
            ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate
        };

        // Act
        HttpResponseMessage responseMessage = await _authenticatedServer
            .CreateClient()
            .AuthenticateWith(_performer)
            .PutAsync(ApiEndpoints.MyProjectsController.SetParticipation(project.Id), BuildStringContent(dto));

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        MyProjectParticipationDto result = await DeserializeResponseMessageAsync<MyProjectParticipationDto>(responseMessage);
        _ = result.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.Id));
        _ = result.Id.Should().NotBeEmpty();
    }
}
