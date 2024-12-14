using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Enums;

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

        private Mapper _mapper;

        [Test]
        public void Should_Map_MusicianProfileCreateDto_To_CreateMusicianProfileCommand()
        {
            // Arrange
            var dto = new MyMusicianProfileCreateDto
            {
                LevelAssessmentInner = 1,
                InstrumentId = Guid.NewGuid(),
                InquiryStatusInner = MusicianProfileInquiryStatus.EmergencyOnly,
            };
            dto.PreferredPositionsInnerIds.Add(Guid.NewGuid());
            dto.PreferredPartsInner.Add(4);

            // Act
            CreateMusicianProfile.Command command = _mapper.Map<CreateMusicianProfile.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(dto);
        }
    }
}
