using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.DoublingInstrumentApplication.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Enums;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class MusicianProfileCreateDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MusicianProfileCreateDtoMappingProfile>();
                cfg.AddProfile<DoublingInstrumentCreateDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new MusicianProfileCreateDto
            {
                Id = Guid.NewGuid(),
                Body = new MusicianProfileCreateBodyDto
                {
                    LevelAssessmentInner = 1,
                    LevelAssessmentTeam = 2,
                    InstrumentId = Guid.NewGuid(),
                    QualificationId = Guid.NewGuid(),
                    InquiryStatusInner = MusicianProfileInquiryStatus.Unknown,
                    InquiryStatusTeam = MusicianProfileInquiryStatus.EmergencyOnly,
                }
            };
            dto.Body.PreferredPositionsInnerIds.Add(Guid.NewGuid());
            dto.Body.PreferredPositionsTeamIds.Add(Guid.NewGuid());
            dto.Body.DoublingInstruments.Add(new DoublingInstrumentCreateBodyDto
            {
                AvailabilityId = Guid.NewGuid(),
                Comment = "Comment",
                InstrumentId = Guid.NewGuid(),
                LevelAssessmentInner = 2,
                LevelAssessmentTeam = 3
            });
            dto.Body.PreferredPartsInner.Add(4);
            dto.Body.PreferredPartsTeam.Add(2);

            // Act
            CreateMusicianProfile.Command command = _mapper.Map<CreateMusicianProfile.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(dto.Body, opt => opt.Excluding(c => c.DoublingInstruments));
            _ = command.PersonId.Should().Be(dto.Id);
        }
    }
}
