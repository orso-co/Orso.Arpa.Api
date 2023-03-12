using System;

namespace Orso.Arpa.Misc.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToIsoString(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ? null : dateTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
        }
    }
}
