using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Appointments;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class AppointmentSeedData
    {
        public static IList<Appointment> Appointments
        {
            get
            {
                return new List<Appointment>
                {
                    RockingXMasRehearsal,
                    RockingXMasConcert,
                    AfterShowParty,
                    StaffMeeting,
                    PhotoSession,
                    RehearsalWeekend,
                    AuditionDays
                };
            }
        }

        public static Appointment RockingXMasRehearsal
        {
            get
            {
                var id = Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3");
                var appointment = new Appointment
                (
                    id,
                    new Create.Command
                    {
                        CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[0].Id,
                        StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[0].Id,
                        SalaryId = SelectValueMappingSeedData.AppointmentSalaryMappings[0].Id,
                        SalaryPatternId = SelectValueMappingSeedData.AppointmentSalaryPatternMappings[0].Id,
                        StartTime = new DateTime(2019, 12, 21, 10, 00, 00),
                        EndTime = new DateTime(2019, 12, 21, 18, 30, 00),
                        PublicDetails = "Let's rock",
                        InternalDetails = "I need more coffee",
                        Name = "Rocking X-mas Dress Rehearsal",
                        ExpectationId = SelectValueMappingSeedData.AppointmentExpectationMappings[2].Id
                    }
                );
                appointment.ProjectAppointments.Add(new ProjectAppointment(
                    Guid.Parse("677356ab-650c-4d44-be4f-b6c61528c356"),
                    ProjectSeedData.RockingXMas.Id,
                    id));
                appointment.SetProperty(nameof(Appointment.VenueId), Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"));
                return appointment;
            }
        }

        public static Appointment RockingXMasConcert
        {
            get
            {
                var id = Guid.Parse("bcf930c0-18d5-48b4-ab10-d477a8cb822f");
                var appointment = new Appointment
                (
                    id,
                    new Create.Command
                    {
                        CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[1].Id,
                        StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[1].Id,
                        SalaryId = SelectValueMappingSeedData.AppointmentSalaryMappings[1].Id,
                        SalaryPatternId = SelectValueMappingSeedData.AppointmentSalaryPatternMappings[1].Id,
                        StartTime = new DateTime(2019, 12, 22, 20, 00, 00),
                        EndTime = new DateTime(2019, 12, 22, 23, 30, 00),
                        PublicDetails = "Sold out :-)",
                        InternalDetails = "Where is my jacket?",
                        Name = "Rocking X-mas Concert",
                        ExpectationId = SelectValueMappingSeedData.AppointmentExpectationMappings[1].Id
                    }
                );
                appointment.SetProperty(nameof(Appointment.VenueId), Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"));
                return appointment;
            }
        }

        public static Appointment AfterShowParty
        {
            get
            {
                var appointmentId = Guid.Parse("2aeb552b-81db-4989-9578-35e1616a4345");
                var appointment = new Appointment
                (
                    appointmentId,
                    new Create.Command
                    {
                        CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[2].Id,
                        StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[2].Id,
                        SalaryId = SelectValueMappingSeedData.AppointmentSalaryMappings[2].Id,
                        SalaryPatternId = SelectValueMappingSeedData.AppointmentSalaryPatternMappings[2].Id,
                        StartTime = new DateTime(2019, 12, 24),
                        EndTime = new DateTime(2019, 12, 24, 06, 00, 00),
                        PublicDetails = "Get the party started",
                        InternalDetails = "Shake it, baby",
                        Name = "Rocking X-mas After Show Party",
                        ExpectationId = SelectValueMappingSeedData.AppointmentExpectationMappings[0].Id
                    }
                );
                appointment.ProjectAppointments.Add(new ProjectAppointment(
                    Guid.Parse("0ce71f0f-26a1-4315-b1f2-0b27c20360fe"),
                    ProjectSeedData.RockingXMas.Id,
                    appointmentId));
                appointment.AppointmentRooms.Add(new AppointmentRoom(appointmentId, RoomSeedData.AulaWeiherhofSchule.Id));
                appointment.SectionAppointments.Add(new SectionAppointment(
                    Guid.Parse("3f6f909d-0786-4bc8-88c7-98fa3316db00"),
                    SectionSeedData.Alto.Id,
                    appointmentId));
                appointment.SetProperty(nameof(Appointment.VenueId), Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"));
                return appointment;
            }
        }

        public static Appointment StaffMeeting
        {
            get
            {
                var id = Guid.Parse("cab05507-489c-4f18-aad5-f1c393626860");
                var appointment = new Appointment
                (
                    id,
                    new Create.Command
                    {
                        CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[0].Id,
                        StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[0].Id,
                        SalaryId = SelectValueMappingSeedData.AppointmentSalaryMappings[1].Id,
                        SalaryPatternId = null,
                        StartTime = new DateTime(2020, 12, 22, 20, 00, 00),
                        EndTime = new DateTime(2020, 12, 22, 23, 30, 00),
                        PublicDetails = "Meet and greet",
                        InternalDetails = "Reminder: Don't forget to talk about the summer holidays",
                        Name = "Team Meeting",
                        ExpectationId = SelectValueMappingSeedData.AppointmentExpectationMappings[2].Id
                    }
                );
                appointment.SetProperty(nameof(Appointment.VenueId), Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"));
                appointment.ProjectAppointments.Add(new ProjectAppointment(
                    Guid.Parse("1d600521-f7e3-4f77-9f97-827d353d7bbf"),
                    ProjectSeedData.HoorayForHollywood.Id,
                    id));
                return appointment;
            }
        }

        public static Appointment PhotoSession
        {
            get
            {
                var id = Guid.Parse("90be67fe-c0b9-4824-9acd-6d8730f3f39b");
                return new Appointment
                (
                    id,
                    new Create.Command
                    {
                        CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[4].Id,
                        StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[0].Id,
                        SalaryId = SelectValueMappingSeedData.AppointmentSalaryMappings[1].Id,
                        SalaryPatternId = null,
                        StartTime = new DateTime(2020, 12, 22, 15, 00, 00),
                        EndTime = new DateTime(2020, 12, 22, 16, 00, 00),
                        PublicDetails = "Photo session for season to come",
                        InternalDetails = null,
                        Name = "Photo session",
                        ExpectationId = SelectValueMappingSeedData.AppointmentExpectationMappings[1].Id
                    }
                );
            }
        }

        public static Appointment RehearsalWeekend
        {
            get
            {
                var id = Guid.Parse("f14e47d8-110f-4346-87d2-9a9bc0e2120c");
                var appointment = new Appointment
                (
                    id,
                    new Create.Command
                    {
                        CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[4].Id,
                        StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[0].Id,
                        SalaryId = SelectValueMappingSeedData.AppointmentSalaryMappings[1].Id,
                        SalaryPatternId = null,
                        EndTime = new DateTime(2019, 12, 24, 16, 00, 00),
                        StartTime = new DateTime(2019, 12, 20, 15, 00, 00),
                        PublicDetails = "Accordion rehearsal weekend",
                        InternalDetails = null,
                        Name = "Rehearsal weekend",
                        ExpectationId = SelectValueMappingSeedData.AppointmentExpectationMappings[1].Id
                    }
                );
                appointment.SectionAppointments.Add(new SectionAppointment(
                    Guid.Parse("460792f7-2055-4885-b180-d3cde690a1bf"),
                    SectionSeedData.Accordion.Id,
                    id));
                return appointment;
            }
        }

        public static Appointment AuditionDays
        {
            get
            {
                var id = Guid.Parse("51d24e3b-d258-4855-bc5a-3c05fb661636");
                var appointment = new Appointment
                (
                    id,
                    new Create.Command
                    {
                        CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[4].Id,
                        StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[0].Id,
                        SalaryId = SelectValueMappingSeedData.AppointmentSalaryMappings[1].Id,
                        SalaryPatternId = null,
                        StartTime = new DateTime(2020, 11, 29, 8, 00, 00),
                        EndTime = new DateTime(2020, 12, 2, 17, 00, 00),
                        PublicDetails = "Audition days for piccolo flutes",
                        InternalDetails = null,
                        Name = "Audition days",
                        ExpectationId = SelectValueMappingSeedData.AppointmentExpectationMappings[1].Id
                    }
                );
                appointment.SectionAppointments.Add(new SectionAppointment(
                    Guid.Parse("bbc705d7-a63a-42a1-832c-b006db483d43"),
                    SectionSeedData.PiccoloFlute.Id,
                    id));
                return appointment;
            }
        }
    }
}
