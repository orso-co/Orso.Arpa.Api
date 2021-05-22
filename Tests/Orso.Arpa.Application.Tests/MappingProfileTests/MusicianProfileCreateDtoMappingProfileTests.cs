using System;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class MusicianProfileCreateDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MusicianProfileCreateDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            MusicianProfileCreateDto dto = new Faker<MusicianProfileCreateDto>()
                .RuleFor(dto => dto.Id, Guid.NewGuid())

                .Generate();

            dto.Body = new Faker<MusicianProfileCreateBodyDto>()
                .RuleFor(body => body.LevelAssessmentPerformer, (byte)1)
                .Generate();

            dto.Body.LevelAssessmentPerformer = (byte)1;
            dto.Body.LevelAssessmentStaff = (byte)2;
            dto.Body.InstrumentId = Guid.NewGuid();
            dto.Body.QualificationId = Guid.NewGuid();
            dto.Body.InquiryStatusPerformerId = Guid.NewGuid();
            dto.Body.InquiryStatusStaffId = Guid.NewGuid();

            // ToDo collections

            // Act
            Create.Command command = _mapper.Map<Create.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto.Body);
            command.PersonId.Should().Be(dto.Id);
        }
    }
}
