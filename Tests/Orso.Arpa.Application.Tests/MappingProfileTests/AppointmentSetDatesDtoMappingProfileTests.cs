using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Domain.Logic.Appointments;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentSetDatesDtoMappingProfileTests : DtoMappingProfileTestBase
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentSetDatesDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentSetDatesDto
            {
                Id = Guid.NewGuid(),
                StartTime = DateTimeProvider.Instance.GetUtcNow(),
                EndTime = DateTimeProvider.Instance.GetUtcNow().AddHours(2)
            };

            // Act
            SetDates.Command command = _mapper.Map<SetDates.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
