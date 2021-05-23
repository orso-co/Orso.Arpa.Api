using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Logic.Projects;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ProjectModifyDtoMappingProfileTests
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
            var dto = new ProjectModifyDto
            {
                Id = Guid.NewGuid(),
                Body = new ProjectModifyBodyDto
                {
                    Title = "Title",
                    ShortTitle = "Short Title",
                    Description = "Description",
                    Code = "0815ABC",
                    TypeId = Guid.NewGuid(),
                    GenreId = Guid.NewGuid(),
                    StartDate = new DateTime(2022, 03, 03),
                    EndDate = new DateTime(2022, 04, 04),
                    StateId = Guid.NewGuid(),
                    ParentId = Guid.NewGuid(),
                    IsCompleted = true
                }
            };
            var expectedCommand = new Modify.Command
            {
                Id = dto.Id,
                Title = dto.Body.Title,
                ShortTitle = dto.Body.ShortTitle,
                Description = dto.Body.Description,
                Code = dto.Body.Code,
                TypeId = dto.Body.TypeId,
                GenreId = dto.Body.GenreId,
                StartDate = dto.Body.StartDate,
                EndDate = dto.Body.EndDate,
                StateId = dto.Body.StateId,
                ParentId = dto.Body.ParentId,
                IsCompleted = dto.Body.IsCompleted
            };

            // Act
            Modify.Command command = _mapper.Map<Modify.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
