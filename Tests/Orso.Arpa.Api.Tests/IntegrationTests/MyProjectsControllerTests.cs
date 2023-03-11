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
        // Act
        HttpResponseMessage responseMessage = await _authenticatedServer
            .CreateClient()
            .AuthenticateWith(_performer)
            .GetAsync(ApiEndpoints.MyProjectsController.Get());

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        IList<MyProjectDto> result = await DeserializeResponseMessageAsync<IList<MyProjectDto>>(responseMessage);
        _ = result.Should().BeEquivalentTo(MyProjectDtoData.PerformerProjects, opt => opt.WithStrictOrdering());
    }

    [Test, Order(2)]
    public async Task Should_Set_My_Project_Participation()
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
        _fakeSmtpServer.ClearReceivedEmail();

        // Act
        HttpResponseMessage responseMessage = await _authenticatedServer
            .CreateClient()
            .AuthenticateWith(_performer)
            .PutAsync(ApiEndpoints.MyProjectsController.SetParticipation(project.Id), BuildStringContent(dto));

        // Assert
        _ = responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        MyProjectParticipationDto result = await DeserializeResponseMessageAsync<MyProjectParticipationDto>(responseMessage);
        _ = result.Should().BeEquivalentTo(expectedDto);
        EvaluateSimpleEmail("Interested von Per Former für 1007 - Die Schneekönigin", "kbb@orso.co");
    }
}
