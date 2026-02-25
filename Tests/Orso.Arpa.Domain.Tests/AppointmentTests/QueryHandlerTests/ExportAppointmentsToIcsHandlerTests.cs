using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.AppointmentDomain.Queries;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.QueryHandlerTests;

[TestFixture]
public class ExportAppointmentsToIcsHandlerTests
{
    private IArpaContext _arpaContext;
    private ExportAppointmentsToIcs.Handler _handler;
    private static readonly string[] separator = ["\r\n", "\r", "\n", Environment.NewLine];

    [SetUp]
    public void Setup()
    {
        _arpaContext = Substitute.For<IArpaContext>();
        _handler = new ExportAppointmentsToIcs.Handler(_arpaContext);
    }

    [Test]
    public async Task Should_Export_Appointments_To_Ics()
    {
        // Arrange
        DbSet<Appointment> appointmentMock =
            new List<Appointment>
            {
                FakeAppointments.RockingXMasRehearsal, AppointmentSeedData.RehearsalWeekend
            }.BuildMockDbSet();
        _arpaContext.Appointments.Returns(appointmentMock);

        // Act
        string result = await _handler.Handle(new ExportAppointmentsToIcs.Query(), new CancellationToken());
        string[] normalizedResult = RemoveDynamicLines(result);

        // Assert: Check structural elements are present
        normalizedResult.Should().Contain("BEGIN:VCALENDAR");
        normalizedResult.Should().Contain("END:VCALENDAR");
        normalizedResult.Should().Contain("BEGIN:VTIMEZONE");
        normalizedResult.Should().Contain("TZID:Europe/Berlin");
        normalizedResult.Should().Contain("END:VTIMEZONE");

        // VTIMEZONE must have STANDARD and/or DAYLIGHT components (Outlook requirement)
        var hasStandardOrDaylight = normalizedResult.Any(l =>
            l.StartsWith("BEGIN:STANDARD") || l.StartsWith("BEGIN:DAYLIGHT"));
        hasStandardOrDaylight.Should().BeTrue("VTIMEZONE must include STANDARD or DAYLIGHT components for Outlook compatibility");

        // Check events
        normalizedResult.Should().Contain("SUMMARY:Rocking X-mas Dress Rehearsal");
        normalizedResult.Should().Contain("SUMMARY:Rehearsal weekend");
        normalizedResult.Should().Contain("DESCRIPTION:Rehearsal | Let's rock | I need more coffee");
        normalizedResult.Should().Contain("LOCATION:Freiburg");

        // Check timezone-aware date-time format (TZID parameter)
        result.Should().Contain("DTSTART;TZID=Europe/Berlin:");
        result.Should().Contain("DTEND;TZID=Europe/Berlin:");
    }

    private static string[] RemoveDynamicLines(string icsContent)
    {
        var lines = icsContent.Split(separator, StringSplitOptions.None);
        return lines
            .Where(line =>
                !line.StartsWith("DTSTAMP")
                && !line.StartsWith("UID")
                && !string.IsNullOrEmpty(line))
            .ToArray();
    }
}
