using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Domain.Logic.Appointments;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentSetDatesDtoMappingProfileTests
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
                Body = new AppointmentSetDatesBodyDto
                {
                    StartTime = FakeDateTime.UtcNow,
                    EndTime = FakeDateTime.UtcNow.AddHours(2)
                }
            };
            var expectedCommand = new SetDates.Command
            {
                Id = dto.Id,
                StartTime = dto.Body.StartTime,
                EndTime = dto.Body.EndTime
            };

            // Act
            SetDates.Command command = _mapper.Map<SetDates.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
