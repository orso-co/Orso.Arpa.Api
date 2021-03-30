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
                .RuleFor(dto => dto.Id, Guid.NewGuid())
                .RuleFor(dto => dto.Title, (f) => f.Lorem.Paragraph(50))
                .RuleFor(dto => dto.ShortTitle, (f) => f.Lorem.Paragraph(8))
                .RuleFor(dto => dto.Description, (f) => f.Lorem.Paragraph())
                .RuleFor(dto => dto.Number, "0815ABC")
                .RuleFor(dto => dto.TypeId, Guid.NewGuid())
                .RuleFor(dto => dto.GenreId, Guid.NewGuid())
                .RuleFor(dto => dto.StartDate, new DateTime(2022, 03, 03))
                .RuleFor(dto => dto.EndDate, new DateTime(2022, 04, 04))
                .RuleFor(dto => dto.StateId, Guid.NewGuid())
                .RuleFor(dto => dto.ParentId, Guid.NewGuid())
                .RuleFor(dto => dto.IsCompleted, true)
                .Generate();

            // Act
            Modify.Command command = _mapper.Map<Modify.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
