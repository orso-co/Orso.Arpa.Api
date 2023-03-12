using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Misc.Logging;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Misc.Tests.Logging
{
    [TestFixture]
    public class KbbInfoLogFormatterTests
    {
        [Test]
        public void Should_Format_Kbb_Info_log()
        {
            // Arrange
            const string header = "my special header";
            var infoLines = new Dictionary<string, object>
            {
                {  "Musician Profile", FakeMusicianProfiles.PerformerMusicianProfile },
                {  "Person", PersonTestSeedData.Performer }
            };
            const string subHeader = "subheader";
            const string expectedResult = "*MY SPECIAL HEADER*\n_subheader_\n\n>Musician Profile: {MusicianProfile}\n>Person: {Person}\n\n#KBB#";

            // Act
            var result = KbbInfoLogFormatter.FormatLog(
                header,
                infoLines,
                subHeader);

            // Assert
            _ = result.Should().Be(expectedResult);
        }
    }
}
