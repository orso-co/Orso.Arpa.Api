using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class MusicianProfileDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MusicianProfileDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<DoublingInstrumentDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            MusicianProfile musicianProfile = FakeMusicianProfiles.PerformerHornMusicianProfile;
            MusicianProfileDto expectedDto = MusicianProfileDtoData.PerformersHornMusicianProfile;

            // Act
            MusicianProfileDto mappedDto = _mapper.Map<MusicianProfileDto>(musicianProfile);

            // Assert
            mappedDto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
