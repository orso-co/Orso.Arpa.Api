using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

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

        private IMapper _mapper;

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
                    IsDeactivated = false,

                    LevelAssessmentInner = 1,
                    LevelAssessmentTeam = 2,
                    ProfilePreferenceInner = 3,
                    ProfilePreferenceTeam = 4,

                    BackgroundInner = "Performer gave some background",
                    BackgroundTeam = "Staff gave some background",
                    SalaryComment = "Perfomer only accepty PayPal",

                    QualificationId = Guid.NewGuid(),
                    SalaryId = Guid.NewGuid(),
                    InquiryStatusInnerId = Guid.NewGuid(),
                    InquiryStatusTeamId = Guid.NewGuid(),

                    // ToDo for collections
                }
            };
            var expectedCommand = new Modify.Command
            {
                Id = dto.Id,
                IsMainProfile = dto.Body.IsMainProfile,
                IsDeactivated = dto.Body.IsDeactivated,

                LevelAssessmentInner = dto.Body.LevelAssessmentInner,
                LevelAssessmentTeam = dto.Body.LevelAssessmentTeam,
                ProfilePreferenceInner = dto.Body.ProfilePreferenceInner,
                ProfilePreferenceTeam = dto.Body.ProfilePreferenceTeam,

                BackgroundInner = dto.Body.BackgroundInner,
                BackgroundTeam = dto.Body.BackgroundTeam,
                SalaryComment = dto.Body.SalaryComment,

                QualificationId = dto.Body.QualificationId,
                SalaryId = dto.Body.SalaryId,
                InquiryStatusInnerId = dto.Body.InquiryStatusInnerId,
                InquiryStatusTeamId = dto.Body.InquiryStatusTeamId,

                // ToDo for collections
            };

            // Act
            Modify.Command command = _mapper.Map<Modify.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
