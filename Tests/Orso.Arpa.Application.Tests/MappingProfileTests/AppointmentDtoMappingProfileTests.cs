using System.Linq;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Application.RoleApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentDtoMappingProfileTests : DtoMappingProfileTestBase
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AppointmentDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<ProjectDtoMappingProfile>();
                cfg.AddProfile<UrlDtoMappingProfile>();
                cfg.AddProfile<RoleDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Appointment appointment = FakeAppointments.RockingXMasRehearsal;
            appointment.ProjectAppointments.First().Project.Urls.Remove(appointment.ProjectAppointments.First().Project.Urls.First());
            AppointmentDto expectedDto = AppointmentDtoData.RockingXMasRehearsalForPerformer;

            // Act
            AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto, opt => opt
                .Excluding(dto => dto.Participations));
        }
    }
}
