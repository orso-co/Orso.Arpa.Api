using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.EntityTests
{
    [TestFixture]
    public class MusicianProfileTests
    {
        [Test]
        public void Should_Set_Is_Main_Profile_To_False()
        {
            Entities.MusicianProfile profile = MusicianProfileSeedData.PerformerMusicianProfile;
            profile.IsMainProfile.Should().BeTrue();

            profile.TurnOffIsMainProfileFlag();

            profile.IsMainProfile.Should().BeFalse();
        }
    }
}
