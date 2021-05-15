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
                .RuleFor(dto => dto.LevelAssessmentPerformer, (byte)1)
                .RuleFor(dto => dto.LevelAssessmentStaff, (byte)2)

                .RuleFor(dto => dto.PersonId, Guid.NewGuid())

                .RuleFor(dto => dto.InstrumentId, Guid.NewGuid())
                .RuleFor(dto => dto.QualificationId, Guid.NewGuid())
                .RuleFor(dto => dto.InquiryStatusPerformerId, Guid.NewGuid())
                .RuleFor(dto => dto.InquiryStatusStaffId, Guid.NewGuid())

                // ToDo collections

                .Generate();

            // Act
            Create.Command command = _mapper.Map<Create.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
