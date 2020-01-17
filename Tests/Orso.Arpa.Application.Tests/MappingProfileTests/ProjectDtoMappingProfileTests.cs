using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ProjectDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProjectDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Project project = ProjectSeedData.RockingXMas;
            ProjectDto expectedDto = ProjectDtoData.RockingXMas;

            // Act
            ProjectDto dto = _mapper.Map<ProjectDto>(project);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.CreatedBy));
        }
    }
}
