using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SelectValueDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<LocalizeAction<SelectValueMapping, SelectValueDto>>();
            services.AddSingleton(_localizerCache);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<SelectValueDtoMappingProfile>();
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
            SelectValueMapping selectValueMapping = SelectValueMappingSeedData.ProjectGenreMappings[0];
            selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.ClassicalMusic);
            SelectValueDto expectedDto = SelectValueDtoData.ClassicalMusic;

            // Act
            SelectValueDto dto = _mapper.Map<SelectValueDto>(selectValueMapping);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
