using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Domain.Tests.AppointmentTests
{
    [TestFixture]
    public class DateHelperTests
    {
        private static IEnumerable<TestCaseData> StartDateTestSource
        {
            get
            {
                yield return new TestCaseData(new DateTime(2019, 12, 09), DateRange.Week, new DateTime(2019, 12, 09));
                yield return new TestCaseData(new DateTime(2019, 12, 10), DateRange.Week, new DateTime(2019, 12, 09));
                yield return new TestCaseData(new DateTime(2019, 12, 11), DateRange.Week, new DateTime(2019, 12, 09));
                yield return new TestCaseData(new DateTime(2019, 12, 12), DateRange.Week, new DateTime(2019, 12, 09));
                yield return new TestCaseData(new DateTime(2019, 12, 13), DateRange.Week, new DateTime(2019, 12, 09));
                yield return new TestCaseData(new DateTime(2019, 12, 14), DateRange.Week, new DateTime(2019, 12, 09));
                yield return new TestCaseData(new DateTime(2019, 12, 15), DateRange.Week, new DateTime(2019, 12, 09));
                yield return new TestCaseData(new DateTime(2019, 12, 01), DateRange.Month, new DateTime(2019, 12, 01));
                yield return new TestCaseData(new DateTime(2019, 12, 09), DateRange.Month, new DateTime(2019, 12, 01));
                yield return new TestCaseData(new DateTime(2019, 12, 31), DateRange.Month, new DateTime(2019, 12, 01));
                yield return new TestCaseData(new DateTime(2020, 2, 29), DateRange.Month, new DateTime(2020, 2, 1));
                yield return new TestCaseData(new DateTime(2020, 2, 28), DateRange.Day, new DateTime(2020, 2, 28));
                yield return new TestCaseData(new DateTime(2020, 2, 28, 14, 58, 42), DateRange.Day, new DateTime(2020, 2, 28));
                yield return new TestCaseData(new DateTime(2020, 2, 28, 23, 59, 59), DateRange.Day, new DateTime(2020, 2, 28));
            }
        }

        [TestCaseSource(nameof(StartDateTestSource))]
        public void Should_Get_Start_Date(DateTime date, DateRange dateRange, DateTime expectedDate)
        {
            DateTime returnedDate = DateHelper.GetStartTime(date, dateRange);
            returnedDate.Should().Be(expectedDate);
        }

        private static IEnumerable<TestCaseData> EndDateTestSource
        {
            get
            {
                yield return new TestCaseData(new DateTime(2019, 12, 09), DateRange.Week, new DateTime(2019, 12, 15, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2019, 12, 10), DateRange.Week, new DateTime(2019, 12, 15, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2019, 12, 11), DateRange.Week, new DateTime(2019, 12, 15, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2019, 12, 12), DateRange.Week, new DateTime(2019, 12, 15, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2019, 12, 13), DateRange.Week, new DateTime(2019, 12, 15, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2019, 12, 14), DateRange.Week, new DateTime(2019, 12, 15, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2019, 12, 15), DateRange.Week, new DateTime(2019, 12, 15, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2019, 12, 01), DateRange.Month, new DateTime(2019, 12, 31, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2019, 12, 09), DateRange.Month, new DateTime(2019, 12, 31, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2019, 12, 31), DateRange.Month, new DateTime(2019, 12, 31, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2020, 2, 29), DateRange.Month, new DateTime(2020, 2, 29, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2020, 2, 28), DateRange.Day, new DateTime(2020, 2, 28, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2020, 2, 28, 14, 58, 42), DateRange.Day, new DateTime(2020, 2, 28, 23, 59, 59));
                yield return new TestCaseData(new DateTime(2020, 2, 28, 23, 59, 59), DateRange.Day, new DateTime(2020, 2, 28, 23, 59, 59));
            }
        }

        [TestCaseSource(nameof(EndDateTestSource))]
        public void Should_Get_End_Date(DateTime date, DateRange dateRange, DateTime expectedDate)
        {
            DateTime returnedDate = DateHelper.GetEndTime(date, dateRange);
            returnedDate.Should().Be(expectedDate);
        }
    }
}
