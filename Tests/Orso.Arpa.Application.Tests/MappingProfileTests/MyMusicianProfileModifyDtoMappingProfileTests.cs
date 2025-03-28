using System;
using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Enums;

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

        private Mapper _mapper;

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

                    PreferredPartsInner = [1, 4],
                    PreferredPositionsInnerIds = [Guid.NewGuid()],
                }
            };
            var expectedCommand = new ModifyMyMusicianProfile.Command
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
            ModifyMyMusicianProfile.Command command = _mapper.Map<ModifyMyMusicianProfile.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
