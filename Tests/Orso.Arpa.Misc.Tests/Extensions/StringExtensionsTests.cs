using FluentAssertions;
using NUnit.Framework;

namespace Orso.Arpa.Misc.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        public StringExtensionsTests()
        {
        }

        [Test]
        public void ShouldRemoveNonAsciiCharacters()
        {
            var result = "FaDiNa_Auswahl_2020-02-25_85836_©_A.Linsenmann.jpg".RemoveEverythingButAscii();

            result.Should().Be("FaDiNa_Auswahl_2020-02-25_85836__A.Linsenmann.jpg");
        }

        [Test]
        public void CreateGuid_ShouldReturnEmptyGuid_WhenStringIsNull()
        {
            // Arrange
            string? input = null;

            // Act
            Guid result = input.CreateGuid();

            // Assert
            result.Should().Be(Guid.Empty);
        }

        [Test]
        public void CreateGuid_ShouldReturnEmptyGuid_WhenStringIsEmpty()
        {
            // Arrange
            string input = string.Empty;

            // Act
            Guid result = input.CreateGuid();

            // Assert
            result.Should().Be(Guid.Empty);
        }

        [Test]
        public void CreateGuid_ShouldReturnEmptyGuid_WhenStringIsWhitespace()
        {
            // Arrange
            string input = "   ";

            // Act
            Guid result = input.CreateGuid();

            // Assert
            result.Should().Be(Guid.Empty);
        }

        [Test]
        public void CreateGuid_ShouldReturnDeterministicGuid_WhenStringIsValid()
        {
            // Arrange
            string input = "Hello world!";
            Guid expectedGuid = Guid.Parse("9d26fb86-0d19-852c-f6e0-468ceca42a20");

            // Act
            Guid result = input.CreateGuid();

            // Assert
            result.Should().Be(expectedGuid);
        }

        [Test]
        public void CreateGuid_ShouldReturnSameGuid_ForSameString()
        {
            // Arrange
            string input = "sameString";

            // Act
            Guid result1 = input.CreateGuid();
            Guid result2 = input.CreateGuid();

            // Assert
            result1.Should().Be(result2);
        }
    }
}

