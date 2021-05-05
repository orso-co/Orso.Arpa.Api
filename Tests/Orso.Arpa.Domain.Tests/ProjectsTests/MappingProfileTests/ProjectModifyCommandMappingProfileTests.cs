using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Logic.Projects;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.ProjectsTests.MappingProfileTests
{
    [TestFixture]
    public class ProjectModifyCommandMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Modify.MappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Entities.Project sourceProject = FakeProjects.RockingXMas;
            Entities.Project expectedProject = FakeProjects.RockingXMas;
            expectedProject.SetProperty(nameof(Entities.Project.Description), expectedProject.Description + " modified");
            expectedProject.SetProperty(nameof(Entities.Project.Title), expectedProject.Title + " modified");
            var command = new Modify.Command
            {
                Id = sourceProject.Id,
                Title = expectedProject.Title,
                ShortTitle = expectedProject.ShortTitle,
                Description = expectedProject.Description,
                Code = expectedProject.Code,
                TypeId = expectedProject.TypeId,
                GenreId = expectedProject.GenreId,
                StartDate = expectedProject.StartDate,
                EndDate = expectedProject.EndDate,
                StateId = expectedProject.StateId,
                ParentId = expectedProject.ParentId,
                IsCompleted = expectedProject.IsCompleted,
            };

            // Act
            Entities.Project project = _mapper.Map(command, sourceProject);

            // Assert
            project.Should().BeEquivalentTo(expectedProject, opt => opt.Excluding(dto => dto.Urls));
            project.Urls.Should().BeEquivalentTo(expectedProject.Urls, opt => opt.Excluding(dto => dto.UrlRoles));
        }
    }
}
