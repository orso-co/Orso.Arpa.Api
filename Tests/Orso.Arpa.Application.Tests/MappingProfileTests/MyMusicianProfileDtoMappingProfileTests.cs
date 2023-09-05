using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication;
using Orso.Arpa.Application.EducationApplication;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Infrastructure.Localization;
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
            var services = new ServiceCollection();
            services.AddSingleton<LocalizeAction<Section, SectionDto>>();
            services.AddSingleton(_localizerCache);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MyMusicianProfileDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<MyDoublingInstrumentDtoMappingProfile>();
                cfg.AddProfile<EducationDtoMappingProfile>();
                cfg.AddProfile<CurriculumVitaeReferenceDtoMappingProfile>();
                cfg.AddProfile<MusicianProfileDeactivationDtoMappingProfile>();
                cfg.AddProfile<SectionDtoMappingProfile>();
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        private IMapper _mapper;

        private readonly ILocalizerCache _localizerCache = Substitute.For<ILocalizerCache>();

        private static IEnumerable<TestCaseData> _testData
        {
            get
            {
                yield return new TestCaseData(FakeMusicianProfiles.PerformerHornMusicianProfile, MyMusicianProfileDtoData.PerformersHornMusicianProfile);
                yield return new TestCaseData(FakeMusicianProfiles.PerformerDeactivatedTubaProfile, MyMusicianProfileDtoData.PerformersDeactivatedTubaProfile);
            }
        }

        [Test]
        [TestCaseSource(nameof(_testData))]
        public void Should_Map(MusicianProfile source, MyMusicianProfileDto expectedDto)
        {
            // Act
            MyMusicianProfileDto mappedDto = _mapper.Map<MyMusicianProfileDto>(source);

            // Assert
            mappedDto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
