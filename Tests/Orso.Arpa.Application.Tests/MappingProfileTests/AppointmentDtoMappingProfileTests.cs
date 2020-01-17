using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AppointmentDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Domain.Entities.Appointment appointment = AppointmentSeedData.RockingXMasRehearsal;
            AppointmentDto expectedDto = AppointmentDtoData.RockingXMasRehearsal;

            // Act
            AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.CreatedBy));
        }
    }
}
