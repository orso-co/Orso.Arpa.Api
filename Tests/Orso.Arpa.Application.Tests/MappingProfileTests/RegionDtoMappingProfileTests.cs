using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.RegionApplication.Model;
using Orso.Arpa.Domain.RegionDomain.Model;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RegionDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_localizerCache);
            services.AddSingleton<LocalizeAction<Region, RegionDto>>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RegionDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
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
            Region region = RegionSeedData.Berlin;
            RegionDto expectedDto = RegionDtoData.Berlin;

            // Act
            RegionDto dto = _mapper.Map<RegionDto>(region);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
