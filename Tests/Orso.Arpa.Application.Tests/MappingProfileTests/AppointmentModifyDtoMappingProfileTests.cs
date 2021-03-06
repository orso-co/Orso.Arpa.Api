using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Domain.Logic.Appointments;

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
            var dto = new AppointmentModifyDto
            {
                Id = Guid.NewGuid(),
                Body = new AppointmentModifyBodyDto
                {
                    InternalDetails = "Internal Details",
                    PublicDetails = "Public Details",
                    Name = "Name",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    CategoryId = Guid.NewGuid(),
                    SalaryId = Guid.NewGuid(),
                    SalaryPatternId = Guid.NewGuid(),
                    StatusId = Guid.NewGuid()
                }
            };
            var expectedCommand = new Modify.Command
            {
                Id = dto.Id,
                InternalDetails = dto.Body.InternalDetails,
                PublicDetails = dto.Body.PublicDetails,
                Name = dto.Body.Name,
                StartTime = dto.Body.StartTime,
                EndTime = dto.Body.EndTime,
                CategoryId = dto.Body.CategoryId,
                SalaryId = dto.Body.SalaryId,
                SalaryPatternId = dto.Body.SalaryPatternId,
                StatusId = dto.Body.StatusId
            };

            // Act
            Modify.Command command = _mapper.Map<Modify.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
