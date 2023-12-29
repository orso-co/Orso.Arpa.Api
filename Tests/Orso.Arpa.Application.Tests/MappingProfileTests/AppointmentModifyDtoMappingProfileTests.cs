using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentModifyDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentModifyDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

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
                    Status = AppointmentStatus.Ambiguous
                }
            };
            var expectedCommand = new ModifyAppointment.Command
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
                Status = dto.Body.Status
            };

            // Act
            ModifyAppointment.Command command = _mapper.Map<ModifyAppointment.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
