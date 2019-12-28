using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Sections.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RegisterDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SectionDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Section register = SectionSeedData.Alto;
            SectionDto expectedDto = RegisterDtoData.Alto;

            // Act
            SectionDto dto = _mapper.Map<SectionDto>(register);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.CreatedBy));
        }
    }
}
