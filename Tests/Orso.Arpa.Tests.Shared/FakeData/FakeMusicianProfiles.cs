using System.Linq;
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
                profile.ProjectParticipations.Add(FakeProjectParticipations.PerformerSchneeköniginParticipation);
                return profile;
            }
        }

        public static MusicianProfile PerformerHornMusicianProfile
        {
            get
            {
                MusicianProfile profile = MusicianProfileSeedData.PerformersHornMusicianProfile;
                MusicianProfileSection doublingInstrument = profile.DoublingInstruments.First();
                doublingInstrument.SetProperty(nameof(MusicianProfile.CreatedBy), "anonymous");
                doublingInstrument.SetProperty(nameof(MusicianProfile.CreatedAt), FakeDateTime.UtcNow);
                profile.SetProperty(nameof(MusicianProfile.Person), PersonTestSeedData.Performer);
                profile.SetProperty(nameof(MusicianProfile.CreatedBy), "anonymous");
                profile.SetProperty(nameof(MusicianProfile.CreatedAt), FakeDateTime.UtcNow);
                profile.SetProperty(nameof(MusicianProfile.Instrument), FakeSections.Horn);
                Education universityEducation = EducationSeedData.University;
                universityEducation.SetProperty(nameof(MusicianProfile.CreatedBy), "anonymous");
                universityEducation.SetProperty(nameof(MusicianProfile.CreatedAt), FakeDateTime.UtcNow);
                profile.Educations.Add(universityEducation);
                CurriculumVitaeReference mozarteum = CurriculumVitaeReferenceSeedData.Mozarteum;
                mozarteum.SetProperty(nameof(MusicianProfile.CreatedBy), "anonymous");
                mozarteum.SetProperty(nameof(MusicianProfile.CreatedAt), FakeDateTime.UtcNow);
                profile.CurriculumVitaeReferences.Add(mozarteum);
                return profile;
            }
        }
    }
}
