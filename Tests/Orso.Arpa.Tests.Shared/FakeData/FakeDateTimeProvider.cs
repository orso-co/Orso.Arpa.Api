using System;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTime GetUtcNow()
        {
            return FakeDateTime.UtcNow;
        }
    }
}
