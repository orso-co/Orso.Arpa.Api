using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Application.VenueApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class UserAppointmentDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MyAppointmentDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<VenueDtoMappingProfile>();
                cfg.AddProfile<AddressApplication.AddressDtoMappingProfile>();
                cfg.AddProfile<RoomDtoMappingProfile>();
                cfg.AddProfile<ProjectDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Appointment appointment = FakeAppointments.RockingXMas;
            MyAppointmentDto expectedDto = UserAppointmentDtoTestData.OrsianerUserAppointment;

            // Act
            MyAppointmentDto dto = _mapper.Map<MyAppointmentDto>(appointment);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(dto => dto.PredictionId));
        }
    }
}
