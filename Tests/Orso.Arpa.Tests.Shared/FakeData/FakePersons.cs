using System.Linq;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakePersons
    {
        public static Person Performer
        {
            get
            {
                Person person = PersonTestSeedData.Performer;
                person.MusicianProfiles.Add(FakeMusicianProfiles.PerformerMusicianProfile);
                person.StakeholderGroups.First().SetProperty(nameof(PersonSection.Section), SectionSeedData.Performers);
                return person;
            }
        }
    }
}
