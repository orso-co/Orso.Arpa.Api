using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MyProjectApplication;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests;

public class MyProjectsControllerTests : IntegrationTestBase
{
    [Test, Order(1)]
    public async Task Should_Get_My_Projects()
    {
        var expectedDto = new MyProjectDto
        {
            Project = ProjectDtoData.Schneekönigin
        };
        expectedDto.Participations.Add(new MyProjectParticipationDto
        {
            CommentByStaffInner = "Comment by staff",
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            Id = Guid.Parse("429ac181-9b36-4635-8914-faabc5f593ff"),
            MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
            ParticipationStatusInner = SelectValueDtoData.Acceptance,
            ParticipationStatusInternal = SelectValueDtoData.Candidate,
        });
        var expectedDtos = new List<MyProjectDto>
        {
            expectedDto
        };

        // Act
        HttpResponseMessage responseMessage = await _authenticatedServer
            .CreateClient()
            .AuthenticateWith(_performer)
            .GetAsync(ApiEndpoints.MyProjectsController.Get());

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        IList<MyProjectDto> result = await DeserializeResponseMessageAsync<IList<MyProjectDto>>(responseMessage);
        result.Should().BeEquivalentTo(expectedDtos, opt => opt.WithStrictOrdering());
    }

    [Test, Order(2)]
    public async Task Should_Set_My_Project_Participation()
    {
        // Arrange
        Project project = ProjectSeedData.Schneekönigin;

        var dto = new MyProjectParticipationModifyBodyDto
        {
            ParticipationStatusId = SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[0].Id,
            Comment = "Performer comment",
            MusicianProfileId = MusicianProfileSeedData.PerformerMusicianProfile.Id
        };

        var expectedDto = new MyProjectParticipationDto()
        {
            ParticipationStatusInner = new SelectValueDto
            {
                Id = dto.ParticipationStatusId,
                Name = "Interested",
                Description = ""
            },
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            ModifiedAt = FakeDateTime.UtcNow,
            ModifiedBy = "Per Former",
            CommentByPerformerInner = dto.Comment,
            MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
            CommentByStaffInner = "Comment by staff",
            Id = Guid.Parse("429ac181-9b36-4635-8914-faabc5f593ff"),
            ParticipationStatusInternal = new SelectValueDto
            {
                Id = Guid.Parse("b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b"),
                Name = "Candidate",
                Description = ""
            }
        };

        // Act
        HttpResponseMessage responseMessage = await _authenticatedServer
            .CreateClient()
            .AuthenticateWith(_performer)
            .PutAsync(ApiEndpoints.MyProjectsController.SetParticipation(project.Id), BuildStringContent(dto));

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        MyProjectParticipationDto result = await DeserializeResponseMessageAsync<MyProjectParticipationDto>(responseMessage);
        result.Should().BeEquivalentTo(expectedDto);
    }
}
