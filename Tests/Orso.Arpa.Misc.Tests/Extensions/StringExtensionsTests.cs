using System;
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
    }
}

