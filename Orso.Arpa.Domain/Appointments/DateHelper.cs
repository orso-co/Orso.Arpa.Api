using System;
using System.Net;
using Orso.Arpa.Domain.Errors;

namespace Orso.Arpa.Domain.Appointments
{
    public static class DateHelper
    {
        private static DateTime FirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDayOfMonth(DateTime date)
        {
            DateTime lastDay = FirstDayOfMonth(date).AddMonths(1).AddDays(-1);
            return new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 23, 59, 59);
        }

        private static DateTime FirstDayOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private static DateTime LastDayOfWeek(DateTime date)
        {
            DateTime lastDay = FirstDayOfWeek(date).AddDays(6);
            return new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 23, 59, 59);
        }

        public static DateTime GetStartTime(DateTime date, DateRange range)
        {
            return range switch
            {
                DateRange.Month => FirstDayOfMonth(date),
                DateRange.Week => FirstDayOfWeek(date),
                DateRange.Day => date.Date,
                _ => throw new RestException("Requested DateRange is not supported", HttpStatusCode.BadRequest),
            };
        }

        public static DateTime GetEndTime(DateTime date, DateRange range)
        {
            return range switch
            {
                DateRange.Month => LastDayOfMonth(date),
                DateRange.Week => LastDayOfWeek(date),
                DateRange.Day => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59),
                _ => throw new RestException("Requested DateRange is not supported", HttpStatusCode.BadRequest),
            };
        }
    }
}
