using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Application.RoleApplication.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Application.UrlApplication.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ProjectDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<LocalizeAction<Project, ProjectDto>>();
            services.AddSingleton<LocalizeAction<SelectValueMapping, SelectValueDto>>();
            services.AddSingleton<LocalizeAction<Role, RoleDto>>();
            services.AddSingleton(_localizerCache);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ProjectDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<UrlDtoMappingProfile>();
                cfg.AddProfile<RoleDtoMappingProfile>();
                cfg.AddProfile<SelectValueDtoMappingProfile>();
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        private IMapper _mapper;
        private readonly ILocalizerCache _localizerCache = Substitute.For<ILocalizerCache>();

        [Test]
        public void Should_Map()
        {
            // Arrange
            Project project = FakeProjects.RockingXMas;
            project.Urls.Remove(project.Urls.First());
            ProjectDto expectedDto = ProjectDtoData.RockingXMasForPerformer;

            // Act
            ProjectDto dto = _mapper.Map<ProjectDto>(project);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
