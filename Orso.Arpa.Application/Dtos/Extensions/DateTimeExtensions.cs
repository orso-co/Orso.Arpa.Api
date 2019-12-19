using System;

namespace Orso.Arpa.Application.Dtos.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToIsoString(this DateTime dateTime)
        {
            return dateTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
        }
    }
}
