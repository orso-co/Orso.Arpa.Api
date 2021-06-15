using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AddressApplication;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AddressDtoMappingProfileTests : DtoMappingProfileTestBase
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
            Address address = FakeVenues.WeiherhofSchule.Address;
            AddressDto expectedDto = VenueDtoData.WeiherhofSchule.Address;

            // Act
            AddressDto dto = _mapper.Map<AddressDto>(address);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
