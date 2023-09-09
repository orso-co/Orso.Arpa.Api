using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Model;
using Orso.Arpa.Application.DoublingInstrumentApplication.Model;
using Orso.Arpa.Application.EducationApplication.Model;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication.Model;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Infrastructure.Localization;
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
            var services = new ServiceCollection();
            services.AddSingleton<LocalizeAction<Section, SectionDto>>();
            services.AddSingleton(_localizerCache);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MusicianProfileDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<DoublingInstrumentDtoMappingProfile>();
                cfg.AddProfile<CurriculumVitaeReferenceDtoMappingProfile>();
                cfg.AddProfile<EducationDtoMappingProfile>();
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
                yield return new TestCaseData(FakeMusicianProfiles.PerformerHornMusicianProfile, MusicianProfileDtoData.PerformersHornMusicianProfile);
                yield return new TestCaseData(FakeMusicianProfiles.PerformerDeactivatedTubaProfile, MusicianProfileDtoData.PerformersDeactivatedTubaProfile);
            }
        }

        [Test]
        [TestCaseSource(nameof(_testData))]
        public void Should_Map(MusicianProfile source, MusicianProfileDto expectedDto)
        {
            // Act
            MusicianProfileDto mappedDto = _mapper.Map<MusicianProfileDto>(source);

            // Assert
            mappedDto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
