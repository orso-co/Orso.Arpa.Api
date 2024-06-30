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
            }.AsQueryable().BuildMockDbSet();
        _arpaContext.Appointments.Returns(appointmentMock);
        var expectedResult =
            @"BEGIN:VCALENDAR
            PRODID:-//github.com/rianjs/ical.net//NONSGML ical.net 4.0//EN
            VERSION:2.0
            BEGIN:VTIMEZONE
            TZID:Europe/Berlin
            X-LIC-LOCATION:Europe/Berlin
            END:VTIMEZONE
            BEGIN:VEVENT
            DESCRIPTION:Rehearsal - Let's rock - I need more coffee
            DTEND:20191221T193000
            DTSTAMP:20240630T150941Z
            DTSTART:20191221T110000
            LOCATION:Freiburg
            SEQUENCE:0
            SUMMARY:Rocking X-mas Dress Rehearsal
            UID:7fe11a86-6fbd-4f80-b69d-9e7f51b34acd
            END:VEVENT
            BEGIN:VEVENT
            DESCRIPTION:- - Accordion rehearsal weekend - -
            DTEND:20191224T170000
            DTSTAMP:20240630T150941Z
            DTSTART:20191220T160000
            LOCATION:-
            SEQUENCE:0
            SUMMARY:Rehearsal weekend
            UID:9b3607cc-0cf1-458f-b9c8-52913feef140
            END:VEVENT
            END:VCALENDAR
            ";

        // Act
        string result =
            await _handler.Handle(new ExportAppointmentsToIcs.Query(), new CancellationToken());

        // Assert
        result.Should().Be(expectedResult);
    }
}
