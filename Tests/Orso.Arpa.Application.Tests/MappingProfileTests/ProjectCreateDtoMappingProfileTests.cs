using System;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Enums;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ProjectCreateDtoMappingProfileTests
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
                .RuleFor(dto => dto.Code, "0815ABC")
                .RuleFor(dto => dto.TypeId, Guid.NewGuid())
                .RuleFor(dto => dto.GenreId, Guid.NewGuid())
                .RuleFor(dto => dto.StartDate, new DateTime(2022, 03, 03))
                .RuleFor(dto => dto.EndDate, new DateTime(2022, 04, 04))
                .RuleFor(dto => dto.Status, (f) => f.Random.Enum<ProjectStatus>())
                .RuleFor(dto => dto.IsCompleted, true)
                .RuleFor(dto => dto.ParentId, (f) => f.Random.Guid())
                .Generate();

            // Act
            CreateProject.Command command = _mapper.Map<CreateProject.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(dto);
        }
    }
}
