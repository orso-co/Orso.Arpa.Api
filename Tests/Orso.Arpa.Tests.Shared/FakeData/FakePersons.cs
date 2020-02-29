using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakePersons
    {
        public static Person Orsianer
        {
            get
            {
                Person person = PersonSeedData.Orsianer;
                person.MusicianProfiles.Add(FakeMusicianProfiles.OrsianerMusicianProfile);
                return person;
            }
        }
    }
}
