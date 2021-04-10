using System;

namespace Orso.Arpa.Misc
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
