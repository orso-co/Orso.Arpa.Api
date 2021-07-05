using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication;
using Orso.Arpa.Application.EducationApplication;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class MyMusicianProfileDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MyMusicianProfileDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<MyDoublingInstrumentDtoMappingProfile>();
                cfg.AddProfile<EducationDtoMappingProfile>();
                cfg.AddProfile<CurriculumVitaeReferenceDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            MusicianProfile musicianProfile = FakeMusicianProfiles.PerformerHornMusicianProfile;
            MyMusicianProfileDto expectedDto = MyMusicianProfileDtoData.PerformersHornMusicianProfile;

            // Act
            MyMusicianProfileDto mappedDto = _mapper.Map<MyMusicianProfileDto>(musicianProfile);

            // Assert
            mappedDto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
