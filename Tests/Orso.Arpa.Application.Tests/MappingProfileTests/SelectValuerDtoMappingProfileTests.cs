using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SelectValueDtoMappingProfileTests : DtoMappingProfileTestBase
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SelectValueDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

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
