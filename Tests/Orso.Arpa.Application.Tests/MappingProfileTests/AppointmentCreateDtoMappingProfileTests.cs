using System;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentCreateDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentCreateDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            AppointmentCreateDto dto = new Faker<AppointmentCreateDto>()
                .RuleFor(dto => dto.InternalDetails, f => f.Lorem.Paragraph())
                .RuleFor(dto => dto.PublicDetails, f => f.Lorem.Paragraph())
                .RuleFor(dto => dto.Name, f => f.Name.FirstName())
                .RuleFor(dto => dto.StartTime, f => f.Date.Soon())
                .RuleFor(dto => dto.EndTime, f => f.Date.Soon())
                .RuleFor(dto => dto.CategoryId, _ => Guid.NewGuid())
                .RuleFor(dto => dto.SalaryId, _ => Guid.NewGuid())
                .RuleFor(dto => dto.SalaryPatternId, _ => Guid.NewGuid())
                .RuleFor(dto => dto.Status, f => f.Random.Enum<AppointmentStatus>())
                .RuleFor(dto => dto.ExpectationId, _ => Guid.NewGuid())
                .Generate();

            // Act
            Create.Command command = _mapper.Map<Create.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(dto);
        }
    }
}
