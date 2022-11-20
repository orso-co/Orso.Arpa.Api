using System;
using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class MyMusicianProfileModifyDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MyMusicianProfileModifyDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new MyMusicianProfileModifyDto
            {
                Id = Guid.NewGuid(),
                Body = new MyMusicianProfileModifyBodyDto
                {
                    IsMainProfile = true,

                    LevelAssessmentInner = 1,
                    ProfilePreferenceInner = 3,

                    BackgroundInner = "Performer gave some background",

                    InquiryStatusInner = MusicianProfileInquiryStatus.Unknown,

                    PreferredPartsInner = new List<byte> { 1, 4 },
                    PreferredPositionsInnerIds = new List<Guid> { Guid.NewGuid() },
                }
            };
            var expectedCommand = new ModifyMusicianProfile.Command
            {
                Id = dto.Id,
                IsMainProfile = dto.Body.IsMainProfile,

                LevelAssessmentInner = dto.Body.LevelAssessmentInner,
                ProfilePreferenceInner = dto.Body.ProfilePreferenceInner,

                BackgroundInner = dto.Body.BackgroundInner,

                InquiryStatusInner = dto.Body.InquiryStatusInner,

                PreferredPartsInner = dto.Body.PreferredPartsInner,
                PreferredPositionsInnerIds = dto.Body.PreferredPositionsInnerIds
            };

            // Act
            ModifyMusicianProfile.Command command = _mapper.Map<ModifyMusicianProfile.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
