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
            MusicianProfileModifyDto dto = new Faker<MusicianProfileModifyDto>()
                .RuleFor(dto => dto.Id, Guid.NewGuid())

                .RuleFor(dto => dto.IsMainProfile, true)
                .RuleFor(dto => dto.IsDeactivated, false)

                .RuleFor(dto => dto.LevelAssessmentPerformer, (byte)1)
                .RuleFor(dto => dto.LevelAssessmentStaff, (byte)2)
                .RuleFor(dto => dto.ProfilePreferencePerformer, (byte)3)
                .RuleFor(dto => dto.ProfilePreferenceStaff, (byte)4)

                .RuleFor(dto => dto.BackgroundPerformer, (f) => f.Lorem.Paragraph(50))
                .RuleFor(dto => dto.BackgroundStaff, (f) => f.Lorem.Paragraph(50))
                .RuleFor(dto => dto.SalaryComment, (f) => f.Lorem.Paragraph(50))

                .RuleFor(dto => dto.PersonId, Guid.NewGuid())
                .RuleFor(dto => dto.InstrumentId, Guid.NewGuid())
                .RuleFor(dto => dto.QualificationId, Guid.NewGuid())
                .RuleFor(dto => dto.SalaryId, Guid.NewGuid())
                .RuleFor(dto => dto.InquiryStatusPerformerId, Guid.NewGuid())
                .RuleFor(dto => dto.InquiryStatusStaffId, Guid.NewGuid())

                // ToDo for collections

                .Generate();

            // Act
            Modify.Command command = _mapper.Map<Modify.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);            //TODO warum sind beide Background* Felder identisch nach dem Mapper???
        }
    }
}
