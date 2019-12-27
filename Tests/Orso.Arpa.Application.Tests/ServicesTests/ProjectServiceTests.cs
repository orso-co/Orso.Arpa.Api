using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.ServiceTests
{
    [TestFixture]
    public class ProjectServiceTests
    {
        private IMediator _subMediator;
        private IMapper _subMapper;

        [SetUp]
        public void SetUp()
        {
            _subMediator = Substitute.For<IMediator>();
            _subMapper = Substitute.For<IMapper>();
        }

        private ProjectService CreateService()
        {
            return new ProjectService(
                _subMediator,
                _subMapper);
        }

        [Test]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            ProjectService service = CreateService();
            bool includeCompleted = false;
            IList<ProjectDto> expectedDtos = ProjectDtoData.Projects;
            _subMapper.Map<IEnumerable<ProjectDto>>(Arg.Any<IEnumerable<Project>>())
                .Returns(expectedDtos);

            // Act
            IEnumerable<ProjectDto> result = await service.GetAsync(
                includeCompleted);

            // Assert
            result.Should().BeEquivalentTo(expectedDtos);
        }
    }
}
