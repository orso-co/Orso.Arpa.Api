using System;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Logic.Projects;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ProjectModifyDtoMappingProfileTests : DtoMappingProfileTestBase
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProjectModifyDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            ProjectModifyDto dto = new Faker<ProjectModifyDto>()
                .RuleFor(dto => dto.Title, (f, u) => f.Lorem.Paragraph(50))
                .RuleFor(dto => dto.Description, (f, u) => f.Lorem.Paragraph())
                .RuleFor(dto => dto.Id, f => Guid.NewGuid())
                .Generate();

            // Act
            Modify.Command command = _mapper.Map<Modify.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
