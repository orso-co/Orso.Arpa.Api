using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AddressDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AddressDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Address address = VenueSeedData.WeiherhofSchule.Address;
            AddressDto expectedDto = VenueDtoData.WeiherhofSchule.Address;

            // Act
            AddressDto dto = _mapper.Map<AddressDto>(address);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
