using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Localization;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.Localization
{
    [TestFixture]
    public class TranslationResultFilterTests
    {
        private LocalizerCache _localizerCache;

        [SetUp]
        public void SetUp()
        {
            _localizerCache = Substitute.For<LocalizerCache>();
        }

        private LocationResultFilter CreateTranslationResultFilter()
        {
            return new LocationResultFilter(
                _localizerCache);
        }

        [Test]
        public void Should_Translate_Single_Section()
        {
            // Arrange
            LocationResultFilter locationResultFilter = CreateTranslationResultFilter();
            SectionApplication.SectionDto obj = SectionDtoData.Alto;
            SectionApplication.SectionDto expectedResult = SectionDtoData.Alto;
            expectedResult.Name = "this was changed";

            // Act
            locationResultFilter.TranslateObject(
                obj, 2);

            // Assert
            obj.Name.Should().Be(expectedResult.Name);
            obj.Id.Should().Be(expectedResult.Id);
        }

        [Test]
        public void Should_Translate_Section_Collection()
        {
            // Arrange
            LocationResultFilter locationResultFilter = CreateTranslationResultFilter();
            var sections = new List<SectionDto>
            {
                SectionDtoData.Alto,
                SectionDtoData.Soloists
            };

            // Act
            locationResultFilter.TranslateObject(
                sections, 2);

            // Assert
            sections.Select(s => s.Name).All(s => s.Equals("this was changed")).Should().BeTrue();
        }

        [Test]
        public void Should_Translate_All_Sections()
        {
            // Arrange
            LocationResultFilter locationResultFilter = CreateTranslationResultFilter();
            IList<SectionDto> sections = SectionDtoData.Sections;

            // Act
            locationResultFilter.TranslateObject(
                sections, 2);

            // Assert
            sections.Select(s => s.Name).All(s => s.Equals("this was changed")).Should().BeTrue();
        }
    }
}
