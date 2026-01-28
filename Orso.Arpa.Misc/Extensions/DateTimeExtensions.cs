using System;
using System.Globalization;

namespace Orso.Arpa.Misc.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToIsoString(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ? null : dateTime.ToString("s", CultureInfo.InvariantCulture) + "Z";
        }

        public static string ToGermanDateTimeString(this DateTime dateTime)
        {
            DateTime berlinDateTime = ConvertToLocalTimeBerlin(dateTime);
            return berlinDateTime.ToString("dddd, dd.MM.yyyy HH:mm", new CultureInfo("en-GB"));
        }

        public static DateTime ConvertToLocalTimeBerlin(DateTime dateTime)
        {
            var berlinTimeZone = GetBerlinTimeZone();
            DateTime berlinDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, berlinTimeZone);
            return berlinDateTime;
        }

        private static TimeZoneInfo GetBerlinTimeZone()
        {
            // Try IANA format first (Linux/macOS), then Windows format
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Europe/Berlin");
            }
            catch (TimeZoneNotFoundException)
            {
                return TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            }
        }

        public static string ToGermanTimeString(this DateTime dateTime) {
            DateTime berlinDateTime = ConvertToLocalTimeBerlin(dateTime);
            return berlinDateTime.ToString("HH:mm", new CultureInfo("en-GB"));
        }

        public static DateTime GetNextMidnight(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, DateTimeKind.Local);
        }
    }
}
