using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ReducedProjectDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ReducedProjectDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Domain.Entities.Project project = ProjectSeedData.HoorayForHollywood;
            var expectedDto = new ReducedProjectDto
            {
                Description = project.Description,
                Code = project.Code,
                Id = project.Id,
                ShortTitle = project.ShortTitle,
                Title = project.Title
            };

            // Act
            ReducedProjectDto dto = _mapper.Map<ReducedProjectDto>(project);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
