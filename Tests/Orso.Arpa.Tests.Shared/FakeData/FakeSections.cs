using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeSections
    {
        public static Section Flute
        {
            get
            {
                Section flute = SectionSeedData.Flute;
                flute.Children.Add(SectionSeedData.PiccoloFlute);
                flute.Children.Add(SectionSeedData.AltoFlute);
                flute.Children.Add(SectionSeedData.TenorFlute);
                flute.Children.Add(SectionSeedData.BassFlute);
                return flute;
            }
        }
    }
}
