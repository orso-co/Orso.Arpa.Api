using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeMusicianProfiles
    {
        public static MusicianProfile PerformerMusicianProfile
        {
            get
            {
                MusicianProfile profile = MusicianProfileSeedData.PerformerMusicianProfile;
                profile.SetProperty(nameof(MusicianProfile.Person), PersonTestSeedData.Performer);
                profile.SetProperty(nameof(MusicianProfile.CreatedBy), "anonymous");
                profile.SetProperty(nameof(MusicianProfile.CreatedAt), FakeDateTime.UtcNow);
                profile.ProjectParticipations.Add(FakeProjectParticipations.PerformerProjectParticipation);
                return profile;
            }
        }
    }
}
