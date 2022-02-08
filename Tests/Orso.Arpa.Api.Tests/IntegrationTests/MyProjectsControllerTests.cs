using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;
using Orso.Arpa.Application.MyProjectApplication;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

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
}
