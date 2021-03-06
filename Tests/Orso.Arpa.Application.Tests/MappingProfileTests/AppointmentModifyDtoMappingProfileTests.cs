using System;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentModifyDtoMappingProfileTests : DtoMappingProfileTestBase
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentModifyDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            AppointmentModifyDto dto = new Faker<AppointmentModifyDto>()
                .RuleFor(dto => dto.InternalDetails, (f, u) => f.Lorem.Paragraph())
                .RuleFor(dto => dto.PublicDetails, (f, u) => f.Lorem.Paragraph())
                .RuleFor(dto => dto.Name, (f, u) => f.Name.FirstName())
                .RuleFor(dto => dto.StartTime, (f, u) => f.Date.Soon())
                .RuleFor(dto => dto.EndTime, (f, u) => f.Date.Soon())
                .RuleFor(dto => dto.CategoryId, f => Guid.NewGuid())
                .RuleFor(dto => dto.EmolumentId, f => Guid.NewGuid())
                .RuleFor(dto => dto.EmolumentPatternId, f => Guid.NewGuid())
                .RuleFor(dto => dto.StatusId, f => Guid.NewGuid())
                .RuleFor(dto => dto.Id, f => Guid.NewGuid())
                .Generate();

            // Act
            Domain.Logic.Appointments.Modify.Command command = _mapper.Map<Domain.Logic.Appointments.Modify.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
