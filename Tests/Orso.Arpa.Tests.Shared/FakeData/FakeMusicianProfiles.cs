using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeMusicianProfiles
    {
        public static MusicianProfile OrsianerMusicianProfile
        {
            get
            {
                MusicianProfile profile = MusicianProfileSeedData.OrsianerMusicianProfile;
                profile.ProjectParticipations.Add(FakeProjectParticipations.OrsianerProjectParticipation);
                return profile;
            }
        }
    }
}
