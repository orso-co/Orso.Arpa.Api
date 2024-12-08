using System;
using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Enums;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class MusicianProfileModifyDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MusicianProfileModifyDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new MusicianProfileModifyDto
            {
                Id = Guid.NewGuid(),
                Body = new MusicianProfileModifyBodyDto
                {
                    IsMainProfile = true,

                    LevelAssessmentInner = 1,
                    LevelAssessmentTeam = 2,
                    ProfilePreferenceInner = 3,
                    ProfilePreferenceTeam = 4,

                    BackgroundInner = "Performer gave some background",
                    BackgroundTeam = "Staff gave some background",
                    SalaryComment = "Perfomer only accepty PayPal",

                    QualificationId = Guid.NewGuid(),
                    SalaryId = Guid.NewGuid(),
                    InquiryStatusInner = MusicianProfileInquiryStatus.EmergencyOnly,
                    InquiryStatusTeam = MusicianProfileInquiryStatus.Unknown,

                    PreferredPartsInner = [1, 4],
                    PreferredPartsTeam = [2, 3],
                    PreferredPositionsInnerIds = [Guid.NewGuid()],
                    PreferredPositionsTeamIds = [Guid.NewGuid()]
                }
            };
            var expectedCommand = new ModifyMusicianProfile.Command
            {
                Id = dto.Id,
                IsMainProfile = dto.Body.IsMainProfile,

                LevelAssessmentInner = dto.Body.LevelAssessmentInner,
                LevelAssessmentTeam = dto.Body.LevelAssessmentTeam,
                ProfilePreferenceInner = dto.Body.ProfilePreferenceInner,
                ProfilePreferenceTeam = dto.Body.ProfilePreferenceTeam,

                BackgroundInner = dto.Body.BackgroundInner,
                BackgroundTeam = dto.Body.BackgroundTeam,
                SalaryComment = dto.Body.SalaryComment,

                QualificationId = dto.Body.QualificationId,
                SalaryId = dto.Body.SalaryId,
                InquiryStatusInner = dto.Body.InquiryStatusInner,
                InquiryStatusTeam = dto.Body.InquiryStatusTeam,

                PreferredPartsInner = dto.Body.PreferredPartsInner,
                PreferredPartsTeam = dto.Body.PreferredPartsTeam,
                PreferredPositionsTeamIds = dto.Body.PreferredPositionsTeamIds,
                PreferredPositionsInnerIds = dto.Body.PreferredPositionsInnerIds
            };

            // Act
            ModifyMusicianProfile.Command command = _mapper.Map<ModifyMusicianProfile.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
