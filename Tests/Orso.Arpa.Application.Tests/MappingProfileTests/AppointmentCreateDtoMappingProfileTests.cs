using System;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentCreateDtoMappingProfileTests : DtoMappingProfileTestBase
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
                .RuleFor(dto => dto.InternalDetails, (f, u) => f.Lorem.Paragraph())
                .RuleFor(dto => dto.PublicDetails, (f, u) => f.Lorem.Paragraph())
                .RuleFor(dto => dto.Name, (f, u) => f.Name.FirstName())
                .RuleFor(dto => dto.StartTime, (f, u) => f.Date.Soon())
                .RuleFor(dto => dto.EndTime, (f, u) => f.Date.Soon())
                .RuleFor(dto => dto.CategoryId, f => Guid.NewGuid())
                .RuleFor(dto => dto.SalaryId, f => Guid.NewGuid())
                .RuleFor(dto => dto.SalaryPatternId, f => Guid.NewGuid())
                .RuleFor(dto => dto.StatusId, f => Guid.NewGuid())
                .RuleFor(dto => dto.ExpectationId, f => Guid.NewGuid())
                .Generate();

            // Act
            Create.Command command = _mapper.Map<Create.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
