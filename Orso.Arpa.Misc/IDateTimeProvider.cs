using System;

namespace Orso.Arpa.Misc
{
    public interface IDateTimeProvider
    {
        DateTime GetUtcNow();
    }
}
