using System;

namespace Orso.Arpa.Application.Dtos.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToIsoString(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
            {
                return null;
            }
            return dateTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
        }
    }
}
