using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RoomDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RoomDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Room room = FakeRooms.AulaWeiherhofSchule;
            RoomDto expectedDto = VenueDtoData.WeiherhofSchule.Rooms[0];

            // Act
            RoomDto dto = _mapper.Map<RoomDto>(room);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
