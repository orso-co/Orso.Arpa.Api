using System;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Domain.AppointmentDomain.Util
{
    public static class DateHelper
    {
        private static DateTime FirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0,0,0, DateTimeKind.Local);
        }

        public static DateTime LastDayOfMonth(DateTime date)
        {
            DateTime lastDay = FirstDayOfMonth(date).AddMonths(1).AddDays(-1);
            return new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 23, 59, 59, DateTimeKind.Local);
        }

        private static DateTime FirstDayOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime firstDay = date.AddDays(-1 * diff);
            return new DateTime(firstDay.Year, firstDay.Month, firstDay.Day, 0, 0, 0, DateTimeKind.Local);
        }

        private static DateTime LastDayOfWeek(DateTime date)
        {
            DateTime lastDay = FirstDayOfWeek(date).AddDays(6);
            return new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 23, 59, 59, DateTimeKind.Local);
        }

        public static DateTime GetStartTime(DateTime date, DateRange range)
        {
            return range switch
            {
                DateRange.Month => FirstDayOfMonth(date),
                DateRange.Week => FirstDayOfWeek(date),
                DateRange.Day => new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Local),
                _ => throw new NotSupportedException("Requested DateRange is not supported"),
            };
        }

        public static DateTime GetEndTime(DateTime date, DateRange range)
        {
            return range switch
            {
                DateRange.Month => LastDayOfMonth(date),
                DateRange.Week => LastDayOfWeek(date),
                DateRange.Day => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, DateTimeKind.Local),
                _ => throw new NotSupportedException("Requested DateRange is not supported"),
            };
        }

        public static bool DoDateRangesIntersect(DateTime date, DateRange range, Tuple<DateTime, DateTime> queryRange)
        {
            DateTime rangeStartTime = GetStartTime(date, range);
            DateTime rangeEndTime = GetEndTime(date, range);

            return queryRange.Item1 <= rangeStartTime && queryRange.Item2 > rangeStartTime
                    || queryRange.Item1 >= rangeStartTime && queryRange.Item2 <= rangeEndTime
                    || queryRange.Item1 <= rangeEndTime && queryRange.Item2 > rangeEndTime;
        }
    }
}
