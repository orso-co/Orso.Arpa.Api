using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.RoleApplication.Model;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RoleDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<LocalizeAction<Role, RoleDto>>();
            services.AddSingleton(_localizerCache);
            services.AddAutoMapper(cfg => cfg.AddProfile<RoleDtoMappingProfile>());

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        private IMapper _mapper;
        private readonly ILocalizerCache _localizerCache = Substitute.For<ILocalizerCache>();

        [Test]
        public void Should_Map()
        {
            // Arrange
            Role role = RoleSeedData.Performer;
            RoleDto expectedDto = RoleDtoData.Performer;

            // Act
            RoleDto dto = _mapper.Map<RoleDto>(role);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
