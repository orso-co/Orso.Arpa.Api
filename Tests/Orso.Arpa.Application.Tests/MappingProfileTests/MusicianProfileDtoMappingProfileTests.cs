using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

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
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            MusicianProfile dto = FakeMusicianProfiles.PerformerMusicianProfile;
            MusicianProfileDto expectedDto = MusicianProfileDtoData.PerformerProfile;
            expectedDto.PersonId = PersonTestSeedData.Performer.Id;

            // Act
            MusicianProfileDto mappedDto = _mapper.Map<MusicianProfileDto>(dto);

            // Assert
            mappedDto.Should().BeEquivalentTo(expectedDto, opt => opt.Excluding(r => r.CreatedAt).Excluding(r => r.CreatedBy));
        }
    }
}
