using System;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Logic.Appointments;
using static Orso.Arpa.Application.Logic.Appointments.Modify;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentModifyDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Dto dto = new Faker<Dto>()
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
            Modify.Command command = _mapper.Map<Modify.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
