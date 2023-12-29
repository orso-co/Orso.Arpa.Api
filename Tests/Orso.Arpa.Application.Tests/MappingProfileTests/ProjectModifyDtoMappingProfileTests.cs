using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Enums;

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

        private Mapper _mapper;

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
                    Status = ProjectStatus.Archived,
                    ParentId = Guid.NewGuid(),
                    IsCompleted = true
                }
            };
            var expectedCommand = new ModifyProject.Command
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
                Status = dto.Body.Status,
                ParentId = dto.Body.ParentId,
                IsCompleted = dto.Body.IsCompleted
            };

            // Act
            ModifyProject.Command command = _mapper.Map<ModifyProject.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
