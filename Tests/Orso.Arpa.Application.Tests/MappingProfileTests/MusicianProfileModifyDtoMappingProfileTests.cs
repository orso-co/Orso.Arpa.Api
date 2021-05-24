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

                    LevelAssessmentPerformer = 1,
                    LevelAssessmentStaff = 2,
                    ProfilePreferencePerformer = 3,
                    ProfilePreferenceStaff = 4,

                    BackgroundPerformer = "Performer gave some background",
                    BackgroundStaff = "Staff gave some background",
                    SalaryComment = "Perfomer only accepty PayPal",

                    QualificationId = Guid.NewGuid(),
                    SalaryId = Guid.NewGuid(),
                    InquiryStatusPerformerId = Guid.NewGuid(),
                    InquiryStatusStaffId = Guid.NewGuid(),

                    // ToDo for collections
                }
            };
            var expectedCommand = new Modify.Command
            {
                Id = dto.Id,
                IsMainProfile = dto.Body.IsMainProfile,
                IsDeactivated = dto.Body.IsDeactivated,

                LevelAssessmentPerformer = dto.Body.LevelAssessmentPerformer,
                LevelAssessmentStaff = dto.Body.LevelAssessmentStaff,
                ProfilePreferencePerformer = dto.Body.ProfilePreferencePerformer,
                ProfilePreferenceStaff = dto.Body.ProfilePreferenceStaff,

                BackgroundPerformer = dto.Body.BackgroundPerformer,
                BackgroundStaff = dto.Body.BackgroundStaff,
                SalaryComment = dto.Body.SalaryComment,

                QualificationId = dto.Body.QualificationId,
                SalaryId = dto.Body.SalaryId,
                InquiryStatusPerformerId = dto.Body.InquiryStatusPerformerId,
                InquiryStatusStaffId = dto.Body.InquiryStatusStaffId,

                // ToDo for collections
            };

            // Act
            Modify.Command command = _mapper.Map<Modify.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
