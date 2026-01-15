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
        string[] expectedResultWithoutDynamicValues = [
            "BEGIN:VCALENDAR",
            "PRODID:-//github.com/ical-org/ical.net//NONSGML ical.net 5.1.1//EN",
            "VERSION:2.0",
            "BEGIN:VTIMEZONE",
            "TZID:Europe/Berlin",
            "X-LIC-LOCATION:Europe/Berlin",
            "END:VTIMEZONE",
            "BEGIN:VEVENT",
            "DESCRIPTION:Rehearsal | Let's rock | I need more coffee",
            "DTEND:20191221T193000",
            "DTSTART:20191221T110000",
            "LOCATION:Freiburg",
            "SEQUENCE:0",
            "SUMMARY:Rocking X-mas Dress Rehearsal",
            "END:VEVENT",
            "BEGIN:VEVENT",
            "DESCRIPTION:- | Accordion rehearsal weekend | -",
            "DTEND:20191224T170000",
            "DTSTART:20191220T160000",
            "LOCATION:-",
            "SEQUENCE:0",
            "SUMMARY:Rehearsal weekend",
            "END:VEVENT",
            "END:VCALENDAR"];

        // Act
        string result = await _handler.Handle(new ExportAppointmentsToIcs.Query(), new CancellationToken());
        string[] normalizedResult = RemoveDtstampAndUid(result);

        // Assert
        normalizedResult.Should().BeEquivalentTo(expectedResultWithoutDynamicValues, opt => opt.WithStrictOrdering());
    }

    private static string[] RemoveDtstampAndUid(string icsContent)
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
