using System.Linq;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.SelectValueMappings.Seed;
using Orso.Arpa.Domain.SelectValues.Seed;
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
            SelectValueMapping selectValueMapping = SelectValueMappingSeedData.ProjectGenreMappings.First();
            selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.ClassicalMusic);
            SelectValueDto expectedDto = SelectValueDtoData.ClassicalMusic;

            // Act
            SelectValueDto dto = _mapper.Map<SelectValueDto>(selectValueMapping);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.CreatedBy));
        }
    }
}
