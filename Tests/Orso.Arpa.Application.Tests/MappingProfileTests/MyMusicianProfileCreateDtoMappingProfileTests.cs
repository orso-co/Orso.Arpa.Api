using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class MyMusicianProfileCreateDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MyMusicianProfileCreateDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new MyMusicianProfileCreateDto
            {
                LevelAssessmentInner = 1,
                InstrumentId = Guid.NewGuid(),
                InquiryStatusInnerId = Guid.NewGuid(),
            };
            dto.PreferredPositionsInnerIds.Add(Guid.NewGuid());
            dto.DoublingInstruments.Add(new MyDoublingInstrumentCreateDto
            {
                AvailabilityId = Guid.NewGuid(),
                Comment = "Comment",
                InstrumentId = Guid.NewGuid(),
                LevelAssessmentInner = 2,
            });
            dto.PreferredPartsInner.Add(4);

            // Act
            Create.Command command = _mapper.Map<Create.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto, opt => opt.Excluding(dto => dto.DoublingInstruments));
        }
    }
}
