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
    public class ProjectCreateDtoMappingProfileTests : DtoMappingProfileTestBase
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProjectCreateDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            ProjectCreateDto dto = new Faker<ProjectCreateDto>()
                .RuleFor(dto => dto.Title, (f) => f.Lorem.Paragraph(50))
                .RuleFor(dto => dto.ShortTitle, (f) => f.Lorem.Paragraph(8))
                .RuleFor(dto => dto.Description, (f) => f.Lorem.Paragraph())
                .RuleFor(dto => dto.Number, "0815ABC")
                .RuleFor(dto => dto.TypeId, Guid.NewGuid())
                .RuleFor(dto => dto.GenreId, Guid.NewGuid())
                .RuleFor(dto => dto.StartDate, new DateTime(2022, 03, 03))
                .RuleFor(dto => dto.EndDate, new DateTime(2022, 04, 04))
                //TODO .RuleFor(dto => dto.Urls, (f, u) => f.Lorem.Paragraph())
                .RuleFor(dto => dto.StateId, Guid.NewGuid())
                //TODO .RuleFor(dto => dto.ParentId, (f, u) => f.Lorem.Paragraph())
                .RuleFor(dto => dto.IsCompleted, true)
                .Generate();

            // Act
            Create.Command command = _mapper.Map<Create.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
